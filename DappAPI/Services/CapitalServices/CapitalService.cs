using AutoMapper;
using DappAPI.Extensions.Enums;
using DappAPI.Repositories;
using DappAPI.Repositories.CapitalRepositories;
using DappAPI.Repositories.UnitOfWork;
using DappAPI.ViewModels;
using DappAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DappAPI.Services.CapitalServices
{
    public class CapitalService : ICapitalService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;
        private readonly Repository<Capital> capitalRepo;
        private readonly Repository<DappUser> userRepo;
        public CapitalService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
            capitalRepo = new CapitalRepository(work.CreateRepository<Capital>());
            userRepo = work.CreateRepository<DappUser>();
        }

        public async Task<CapitalDataViewModel> CancelCapital(long id, string userAddress)
        {
            Capital capital = capitalRepo.FirstOrDefault(x => x.Id == id);
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == userAddress);

            if (capital is null || user is null || capital.Status != CapitalStatus.Pending)
            {
                return null;
            }
            capital.Status = CapitalStatus.Cancelled;
            capital.Approver = user;
            await work.SaveAsync();
            CapitalDataViewModel result = mapper.Map<Capital, CapitalDataViewModel>(capital);
            return result;
        }

        public async Task<CapitalDataViewModel> ConfirmCapital(long id, string userAddress)
        {
            Capital capital = capitalRepo.FirstOrDefault(x => x.Id == id);
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == userAddress);

            if (capital is null || user is null || capital.Status != CapitalStatus.Pending || capital.Creator.PublicAddress == userAddress)
            {
                return null;
            }
            capital.Status = CapitalStatus.Confirmed;
            capital.Approver = user;
            await work.SaveAsync();
            CapitalDataViewModel result = mapper.Map<Capital, CapitalDataViewModel>(capital);
            return result;
        }

        public async Task<CapitalDataViewModel> CreateCapital(CreateCapitalViewModel request)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == request.CreatorPublicAddress);
            if (user is null)
            {
                return null;
            }
            Capital capital = mapper.Map<CreateCapitalViewModel, Capital>(request);
            capital.Creator = user;
            capital.CreationDate = DateTime.Today;
            capital.Status = CapitalStatus.Pending;
            capitalRepo.Add(capital);
            await work.SaveAsync();
            CapitalDataViewModel result = mapper.Map<Capital, CapitalDataViewModel>(capital);
            return result;
        }

        public List<CapitalDataViewModel> GetAllCapitals()
        {
            List<Capital> capitals = capitalRepo.GetAll();
            if (capitals is null)
            {
                return null;
            }
            List<CapitalDataViewModel> result = mapper.Map<List<Capital>, List<CapitalDataViewModel>>(capitals);
            return result;
        }

        public List<CapitalDataViewModel> GetCapitalsByApprover(string approverAddress)
        {
            List<Capital> capitals = capitalRepo.Get(x => x.Approver.PublicAddress == approverAddress);
            if (capitals is null)
            {
                return null;
            }
            List<CapitalDataViewModel> result = mapper.Map<List<Capital>, List<CapitalDataViewModel>>(capitals);
            return result;
        }

        public List<CapitalDataViewModel> GetCapitalsByCreator(string creatorAddress)
        {
            List<Capital> capitals = capitalRepo.Get(x => x.Creator.PublicAddress == creatorAddress);
            if (capitals is null)
            {
                return null;
            }
            List<CapitalDataViewModel> result = mapper.Map<List<Capital>, List<CapitalDataViewModel>>(capitals);
            return result;
        }

        public CapitalDataViewModel GetCapitalsById(long id)
        {
            Capital capital = capitalRepo.FirstOrDefault(x => x.Id == id);
            if (capital is null)
            {
                return null;
            }
            CapitalDataViewModel result = mapper.Map<Capital, CapitalDataViewModel>(capital);
            return result;
        }

        public List<CapitalDataViewModel> GetCapitalsByKeyword(string keyword)
        {
            List<Capital> capitals = capitalRepo.Get(x => 
                x.Title.ToUpper().Contains(keyword.ToUpper()) ||
                x.Description.ToUpper().Contains(keyword.ToUpper()));
          
            if (capitals is null)
            {
                return null;
            }
            List<CapitalDataViewModel> result = mapper.Map<List<Capital>, List<CapitalDataViewModel>>(capitals);
            return result;
        }

        public List<CapitalDataViewModel> GetCapitalsByStatus(CapitalStatus status)
        {
            List<Capital> capitals = capitalRepo.Get(x => x.Status == status);
            if (capitals is null)
            {
                return null;
            }
            List<CapitalDataViewModel> result = mapper.Map<List<Capital>, List<CapitalDataViewModel>>(capitals);
            return result;
        }

        public List<CapitalDataViewModel> GetCapitalsByType(CapitalType type)
        {
            List<Capital> capitals = capitalRepo.Get(x => x.Type == type);
            if (capitals is null)
            {
                return null;
            }
            List<CapitalDataViewModel> result = mapper.Map<List<Capital>, List<CapitalDataViewModel>>(capitals);
            return result;
        }

        public List<CapitalDataViewModel> GetCapitalsByValue(double from, double to)
        {
            List<Capital> capitals = capitalRepo.Get(x => from <= x.Value && x.Value <= to);
            if (capitals is null)
            {
                return null;
            }
            List<CapitalDataViewModel> result = mapper.Map<List<Capital>, List<CapitalDataViewModel>>(capitals);
            return result;
        }

        public async Task<CapitalDataViewModel> UpdateCapital(UpdateCapitalViewModel request)
        {
            Capital capital = capitalRepo.FirstOrDefault(x => x.Id == request.Id);
            if (capital is null)
            {
                return null;
            }
            
            capital = mapper.Map<UpdateCapitalViewModel, Capital>(request);
            capitalRepo.Update(capital);
            await work.SaveAsync();
            CapitalDataViewModel result = mapper.Map<Capital, CapitalDataViewModel>(capital);
            return result;
        }
    }
}
