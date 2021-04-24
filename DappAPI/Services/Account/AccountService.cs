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

        public async Task<UserInfoViewModel> CreateUser(RegisterViewModel model)
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
            UserInfoViewModel result = GetUserInfo(user);
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

        public UserInfoViewModel GetUserWithPublicAddress(string publicAddress)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);
            UserInfoViewModel result = GetUserInfo(user);
            return result;
        }

        public async Task<UserInfoViewModel> UpdateUser(UpdateAccountViewModel model)
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
            UserInfoViewModel result = GetUserInfo(user);
            return result;
        }

        public UserInfoViewModel GetUserInfo(string publicAddress)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);
            if (user is null)
            {
                return null;
            }
            UserInfoViewModel result = mapper.Map<DappUser, UserInfoViewModel>(user);
            return result;
        }

        public UserInfoViewModel GetUserInfo(DappUser user)
        {
            DappUser appUser = userRepo.FirstOrDefault(x => x.PublicAddress == user.PublicAddress);
            if (appUser is null)
            {
                return null;
            }
            UserInfoViewModel result = mapper.Map<DappUser, UserInfoViewModel>(appUser);
            return result;
        }

        public List<UserInfoViewModel> GetAllUsersInfo()
        {
            List<DappUser> user = userRepo.GetAll();
            if (user is null)
            {
                return null;
            }
            List<UserInfoViewModel> result = mapper.Map<List<DappUser>, List<UserInfoViewModel>>(user);
            return result;
        }
    }
}
