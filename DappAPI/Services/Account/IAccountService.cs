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

        public Task<List<string>> GetUserRoles(string publicAddress);

        public List<UserInfoViewModel> GetAllUsersInfo();

        public UserInfoViewModel GetUserInfo(string publicAddress);

        public UserInfoViewModel GetUserInfo(DappUser user);

        public Task<UserInfoViewModel> CreateUser(RegisterViewModel model);

        public Task<UserInfoViewModel> UpdateUser(UpdateAccountViewModel model);
    }
}