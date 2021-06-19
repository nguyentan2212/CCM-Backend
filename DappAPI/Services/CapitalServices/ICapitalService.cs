using DappAPI.Extensions.Enums;
using DappAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DappAPI.Services.CapitalServices
{
    public interface ICapitalService
    {
        public List<CapitalDataViewModel> GetAllCapitals();
        public List<CapitalDataViewModel> GetCapitalsByCreator(string creatorAddress);
        public List<CapitalDataViewModel> GetCapitalsByApprover(string approverAddress);
        public List<CapitalDataViewModel> GetCapitalsByAsset(AssetType asset);
        public List<CapitalDataViewModel> GetCapitalsByType(CapitalType type);
        public List<CapitalDataViewModel> GetCapitalsByStatus(CapitalStatus status);
        public List<CapitalDataViewModel> GetCapitalsByValue(double from, double to);
        public List<CapitalDataViewModel> GetCapitalsByKeyword(string keyword);
        public CapitalDataViewModel GetCapitalsById(long id);
        public Task<CapitalDataViewModel> ConfirmCapital(long id, string userAddress);
        public Task<CapitalDataViewModel> CancelCapital(long id, string userAddress);
        public Task<CapitalDataViewModel> CreateCapital(CreateCapitalViewModel request);
        public Task<CapitalDataViewModel> UpdateCapital(UpdateCapitalViewModel request);
    }
}
