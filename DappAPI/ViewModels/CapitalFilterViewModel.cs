using DappAPI.Extensions.Enums;

namespace DappAPI.ViewModels
{
    public class CapitalFilterViewModel
    {
        public string Title { get; set; }
        public AssetType? Asset { get; set; }
        public CapitalType? Type { get; set; }
        public CapitalStatus? Status { get; set; }
        public long? ValueMin { get; set; }
        public long? ValueMax { get; set; }
        public int? count { get; set; }
    }
}
