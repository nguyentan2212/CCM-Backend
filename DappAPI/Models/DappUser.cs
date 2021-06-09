using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DappAPI.Models
{
    public class DappUser : IdentityUser<Guid>
    {       
        [Required]
        public string PublicAddress { get; set; }
        [Required]
        public string FullName { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { set; get; }
        [Required]
        public long Nonce { get; set; }
        [Required]
        public string Address { get; set; }

        public List<Capital> CreatedCapitals { get; set; }
        public List<Capital> ApprovedCapitals { get; set; }
    }
}
