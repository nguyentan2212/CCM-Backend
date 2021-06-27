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
        [Description("Vốn lưu động")]
        Working,
        [Description("None")]
        None
    }

    public enum CapitalStatus
    {
        [Description("Hoàn Thành")]
        Finished,
        [Description("Đã hủy")]
        Cancelled,
        [Description("None")]
        None
    }
}