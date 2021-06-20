using DappAPI.Models;
using DappAPI.Repositories;
using DappAPI.Repositories.UnitOfWork;
using DappAPI.Repositories.AccountRepositories;
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
            userRepo = new AccountReposity(work.CreateRepository<DappUser>());
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
            userRepo.Add(user);
            await work.SaveAsync();
            await userManager.AddToRoleAsync(user, "admin");
            UserDataViewModel result = mapper.Map<DappUser, UserDataViewModel>(user);
            var roles = await GetUserRoles(result.PublicAddress);
            result.Role = roles.FirstOrDefault();
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

        public async Task<List<string>> GetUserRoles(string userId)
        {
            DappUser user = await userManager.FindByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public UserDataViewModel GetUserWithPublicAddress(string publicAddress)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);
            
            UserDataViewModel result = mapper.Map<DappUser, UserDataViewModel>(user);
            var roles = GetUserRoles(result.Id).GetAwaiter().GetResult();
            result.Role = roles.FirstOrDefault();
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
            await work.SaveAsync();
            UserDataViewModel result = mapper.Map<DappUser, UserDataViewModel>(user);
            var roles = GetUserRoles(result.Id).GetAwaiter().GetResult();
            result.Role = roles.FirstOrDefault();
            return result;
        }

        public async Task<UserDataViewModel> GetUserInfo(string userId)
        {
            DappUser user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return null;
            }
            UserDataViewModel result = mapper.Map<DappUser, UserDataViewModel>(user);
            var roles = GetUserRoles(result.Id).GetAwaiter().GetResult();
            result.Role = roles.FirstOrDefault();
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
            foreach (var item in result)
            {
                var roles = GetUserRoles(item.Id).GetAwaiter().GetResult();
                item.Role = roles.FirstOrDefault();
            }
            return result;
        }

        public async Task<UserDataViewModel> Promote(string userId)
        {
            DappUser user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return null;
            }
            await userManager.AddToRoleAsync(user, "admin");
            UserDataViewModel result = mapper.Map<DappUser, UserDataViewModel>(user);
            var roles = GetUserRoles(result.Id).GetAwaiter().GetResult();
            result.Role = roles.FirstOrDefault();
            return result;
        }

        public async Task<UserDataViewModel> Demote(string userId)
        {
            DappUser user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return null;
            }
            await userManager.RemoveFromRoleAsync(user, "admin");
            UserDataViewModel result = mapper.Map<DappUser, UserDataViewModel>(user);
            var roles = GetUserRoles(result.Id).GetAwaiter().GetResult();
            result.Role = roles.FirstOrDefault();
            return result;
        }
    }
}
