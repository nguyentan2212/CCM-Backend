using DappAPI.Extensions.Enums;

namespace DappAPI.ViewModels
{
    public class CapitalFilterViewModel
    {
        public string Title { get; set; }
        public AssetType? Asset { get; set; }
        public CapitalType? Type { get; set; }
        public CapitalStatus? Status { get; set; }
        public double? ValueMin { get; set; }
        public double? ValueMax { get; set; }
        public int? count { get; set; }
    }
}
