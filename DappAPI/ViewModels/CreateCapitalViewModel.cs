using DappAPI.Extensions.Enums;
using System.ComponentModel.DataAnnotations;

namespace DappAPI.ViewModels
{
    public class CreateCapitalViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public long Value { get; set; }
        [Required]
        public AssetType Asset { get; set; }
        [Required]
        public CapitalType Type { get; set; }
        [Required]
        public string CreatorPublicAddress { get; set; }
    }
}
