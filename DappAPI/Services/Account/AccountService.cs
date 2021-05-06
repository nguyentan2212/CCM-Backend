using DappAPI.Models;
using DappAPI.Repositories;
using DappAPI.Repositories.UnitOfWork;
using DappAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace DappAPI.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork work;
        private readonly UserManager<DappUser> userManager;
        private Repository<DappUser> userRepo;
        private readonly IMapper mapper;

        public AccountService(IUnitOfWork work, UserManager<DappUser> userManager, IMapper mapper)
        {
            this.work = work;
            this.userManager = userManager;
            userRepo = work.CreateRepository<DappUser>();
            this.mapper = mapper;
        }

        public async Task<long> ChangeNonce(string publicAddress)
        {        
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);
            if (user is null)
            {
                return 0;
            }
            Random random = new Random();
            user.Nonce = random.Next(10000, 100000);
            await work.SaveAsync();
            return user.Nonce;
        }

        public async Task<UserDataViewModel> CreateUser(RegisterViewModel model)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == model.PublicAddress);
            if (user != null)
            {
                return null;
            }
            user = mapper.Map<RegisterViewModel, DappUser>(model);
            user.CreationDate = DateTime.Today;
            Random random = new Random();
            user.Nonce = random.Next(10000, 100000);
            userRepo.Add(user);
            await work.SaveAsync();
            UserDataViewModel result = GetUserInfo(user);
            return result;
        }

        public long GetNonce(string publicAddress)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);
            if (user is null)
            {
                return 0;
            }
            return user.Nonce;
        }

        public async Task<List<string>> GetUserRoles(string publicAddress)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);
            var roles = await userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public UserDataViewModel GetUserWithPublicAddress(string publicAddress)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);
            UserDataViewModel result = GetUserInfo(user);
            return result;
        }

        public async Task<UserDataViewModel> UpdateUser(UpdateAccountViewModel model)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == model.PublicAddress);
            if (user is null)
            {
                return null;
            }
            user.FullName = model.FullName;
            user.Address = model.Address;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Birthdate = model.Birthdate;
            await work.SaveAsync();
            UserDataViewModel result = GetUserInfo(user);
            return result;
        }

        public UserDataViewModel GetUserInfo(string publicAddress)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);
            if (user is null)
            {
                return null;
            }
            UserDataViewModel result = mapper.Map<DappUser, UserDataViewModel>(user);
            return result;
        }

        public UserDataViewModel GetUserInfo(DappUser user)
        {
            DappUser appUser = userRepo.FirstOrDefault(x => x.PublicAddress == user.PublicAddress);
            if (appUser is null)
            {
                return null;
            }
            UserDataViewModel result = mapper.Map<DappUser, UserDataViewModel>(appUser);
            return result;
        }

        public List<UserDataViewModel> GetAllUsersInfo()
        {
            List<DappUser> user = userRepo.GetAll();
            if (user is null)
            {
                return null;
            }
            List<UserDataViewModel> result = mapper.Map<List<DappUser>, List<UserDataViewModel>>(user);
            return result;
        }
    }
}
