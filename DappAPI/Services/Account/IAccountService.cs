using DappAPI.Models;
using DappAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DappAPI.Services.Account
{
    public interface IAccountService
    {
        public long GetNonce(string publicAddress);

        public Task<long> ChangeNonce(string publicAddress);

        public Task<List<string>> GetUserRoles(string userId);

        public List<UserDataViewModel> GetAllUsersInfo();

        public Task<UserDataViewModel> GetUserInfo(string userId);

        public UserDataViewModel GetUserWithPublicAddress(string publicAddress);

        public Task<string> CreateUser(RegisterViewModel model);

        public Task UpdateUser(UpdateAccountViewModel model);

        public Task Promote(string userId);

        public Task Demote(string userId);

        public Task<bool> IsLockout(string userId);

        public Task LockUser(string userId);

        public Task UnlockUser(string userId);

    }
}