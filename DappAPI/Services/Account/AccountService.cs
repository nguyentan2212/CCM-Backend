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
using DappAPI.Extensions.Exceptions;

namespace DappAPI.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork work;
        private readonly UserManager<DappUser> userManager;
        private readonly RoleManager<UserRole> roleManager;
        private Repository<DappUser> userRepo;
        private Repository<IdentityUserRole<Guid>> userRoleRepo;
        private readonly IMapper mapper;

        public AccountService(IUnitOfWork work, UserManager<DappUser> userManager, RoleManager<UserRole> roleManager, IMapper mapper)
        {
            this.work = work;
            this.userManager = userManager;
            this.roleManager = roleManager;
            userRepo = new AccountReposity(work.CreateRepository<DappUser>());
            userRoleRepo = work.CreateRepository<IdentityUserRole<Guid>>();
            this.mapper = mapper;
        }

        public async Task<long> ChangeNonce(string publicAddress)
        {        
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            Random random = new Random();
            user.Nonce = random.Next(10000, 100000);
            try
            {
                await work.SaveAsync();
            }
            catch
            {
                throw new DataSaveException("Nonce change failed");
            }
            return user.Nonce;
        }

        public async Task<string> CreateUser(RegisterViewModel model)
        {
            try
            {

                DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == model.PublicAddress);
                if (user != null)
                {
                    throw new Exception("Registration failed");
                }
                user = mapper.Map<RegisterViewModel, DappUser>(model);
                userRepo.Add(user);
                Random random = new Random();
                user.Nonce = random.Next(10000, 100000);
                await work.SaveAsync();
                UserRole admin = roleManager.Roles.FirstOrDefault(x => x.Name == "admin");
                userRoleRepo.Add(new IdentityUserRole<Guid>()
                {
                    RoleId = admin.Id,
                    UserId = user.Id
                });
                await work.SaveAsync();
                return user.Id.ToString();
            }
            catch(Exception e)
            {
                throw new DataSaveException("Registration failed");
            }
            
        }

        public long GetNonce(string publicAddress)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            return user.Nonce;
        }

        public async Task<List<string>> GetUserRoles(string userId)
        {
            DappUser user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public UserDataViewModel GetUserWithPublicAddress(string publicAddress)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);           
            UserDataViewModel result = mapper.Map<DappUser, UserDataViewModel>(user);
            result.Role = GetUserRoles(user.Id.ToString()).GetAwaiter().GetResult().LastOrDefault();
            return result;
        }

        public async Task UpdateUser(UpdateAccountViewModel model)
        {
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == model.PublicAddress);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            user.FullName = model.FullName;
            user.Address = model.Address;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            try
            {
                await work.SaveAsync();
            }
            catch
            {
                throw new DataSaveException("User information update failed");
            }
        }

        public async Task<UserDataViewModel> GetUserInfo(string userId)
        {
            DappUser user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            UserDataViewModel result = mapper.Map<DappUser, UserDataViewModel>(user);
            result.Role = GetUserRoles(user.Id.ToString()).GetAwaiter().GetResult().LastOrDefault();
            return result;
        }

        public List<UserDataViewModel> GetAllUsersInfo()
        {
            List<DappUser> user = userRepo.GetAll();
            List<UserDataViewModel> result = mapper.Map<List<DappUser>, List<UserDataViewModel>>(user);
            foreach (var item in result)
            {
                item.Role = GetUserRoles(item.Id).GetAwaiter().GetResult().LastOrDefault();
            }
            return result;
        }

        public async Task Promote(string userId)
        {
            DappUser user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            try
            {
                UserRole admin = roleManager.Roles.FirstOrDefault(x => x.Name == "admin");
                List<IdentityUserRole<Guid>> list = userRoleRepo.GetAll();
                IdentityUserRole<Guid> result = list.FirstOrDefault(x => x.UserId.ToString() == userId);
                userRoleRepo.Remove(result);
                userRoleRepo.Add(new IdentityUserRole<Guid>()
                {
                    RoleId = admin.Id,
                    UserId = user.Id
                });
                await work.SaveAsync();
            }
            catch(Exception e)
            {
                throw new DataSaveException("Failed promotion");
            }
            
        }

        public async Task Demote(string userId)
        {
            DappUser user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            try
            {
                UserRole staff = roleManager.Roles.FirstOrDefault(x => x.Name == "staff");
                List<IdentityUserRole<Guid>> list = userRoleRepo.GetAll();
                IdentityUserRole<Guid> result = list.FirstOrDefault(x => x.UserId.ToString() == userId);
                userRoleRepo.Remove(result);
                userRoleRepo.Add(new IdentityUserRole<Guid>()
                {
                    RoleId = staff.Id,
                    UserId = user.Id
                });
                await work.SaveAsync();
            }
            catch
            {
                throw new DataSaveException("Failed demotion");
            }
            
        }

        public async Task LockUser(string userId)
        {
            DappUser user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            user.IsActive = false;
            try
            {
                await work.SaveAsync();
            }
            catch
            {
                throw new DataSaveException("Failed to lock user");
            }
        }

        public async Task UnlockUser(string userId)
        {
            DappUser user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            user.IsActive = true;
            try
            {
                await work.SaveAsync();
            }
            catch
            {
                throw new DataSaveException("Failed to unlock user");
            }
        }
    }
}
