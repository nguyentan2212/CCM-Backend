using DappAPI.Extensions.Enums;
using DappAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DappAPI.Services.CapitalServices
{
    public interface ICapitalService
    {
        public List<CapitalDataViewModel> GetAllCapitals();
        public List<CapitalDataViewModel> GetCapitalsByCreator(string userId);
        public List<CapitalDataViewModel> GetCapitalsByAsset(AssetType asset);
        public List<CapitalDataViewModel> GetCapitalsByType(CapitalType type);
        public List<CapitalDataViewModel> GetCapitalsByStatus(CapitalStatus status);
        public List<CapitalDataViewModel> GetCapitalsByValue(long from, long to);
        public List<CapitalDataViewModel> GetCapitalsByKeyword(string keyword);
        public CapitalDataViewModel GetCapitalsById(long id);
        public Task<CapitalDataViewModel> CancelCapital(long id);
        public Task<CapitalDataViewModel> CreateCapital(CreateCapitalViewModel request);
        public Task<CapitalDataViewModel> UpdateCapital(UpdateCapitalViewModel request);
        public long GetTotalMoney();
    }
}
