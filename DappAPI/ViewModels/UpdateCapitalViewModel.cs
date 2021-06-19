using DappAPI.Extensions.Enums;
using System.ComponentModel.DataAnnotations;

namespace DappAPI.ViewModels
{
    public class UpdateCapitalViewModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public AssetType Asset { get; set; }
        [Required]
        public CapitalType Type { get; set; }
    }
}
