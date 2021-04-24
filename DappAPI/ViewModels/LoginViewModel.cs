using System.ComponentModel.DataAnnotations;

namespace DappAPI.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string PublicAddress { get; set; }
        [Required]
        public string Signature { get; set; }
    }
}
