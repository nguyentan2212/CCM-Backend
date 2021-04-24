using System;
using System.ComponentModel.DataAnnotations;

namespace DappAPI.ViewModels
{
    public class UpdateAccountViewModel
    {
        [Required]
        public string PublicAddress { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
    }
}