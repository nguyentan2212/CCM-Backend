using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace DappAPI.Models
{
    public class DappUser : IdentityUser<Guid>
    {       
        [Required]
        public string PublicAddress { get; set; }
        [Required]
        public string FullName { set; get; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { set; get; }
        [Required]
        public long Nonce { get; set; }
    }
}
