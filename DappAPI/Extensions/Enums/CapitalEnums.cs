using System.ComponentModel;

namespace DappAPI.Extensions.Enums
{
    public enum AssetType
    {       
        [Description("Tài sản ngắn hạn")]
        ShortTermAsset,
        [Description("Tài sản dài hạn")]
        LongTermAsset,
        [Description("None")]
        None
    }

    public enum CapitalType
    {
        [Description("Vốn chủ sở hữu")]
        Equity,
        [Description("Vốn huy động")]
        Mobilized,
        [Description("None")]
        None
    }

    public enum CapitalStatus
    {
        [Description("Đang đợi")]
        Pending,
        [Description("Đã hủy")]
        Cancelled,
        [Description("Đã xác nhận")]
        Confirmed,
        [Description("None")]
        None
    }
}