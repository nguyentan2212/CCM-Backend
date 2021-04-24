using System.ComponentModel.DataAnnotations;

namespace DappAPI.ViewModels
{
    public class GetNonceViewModel
    {
        [Required]
        public string publicAddress { get; set; }
        [Required]
        public long Nonce { get; set; }
    }
}
