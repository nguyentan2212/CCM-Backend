using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public string Address { get; set; }
        [Required]
        public DateTime CreationDate { set; get; }
        [Required]
        public long Nonce { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public List<Capital> CreatedCapitals { get; set; }

        public DappUser()
        {
            IsActive = true;
        }

        public DappUser(string publicAddress, string fullName, string email, string phoneNumber, string address)
        {
            Random random = new Random();

            PublicAddress = UserName = publicAddress;
            Id = Guid.NewGuid();
            SecurityStamp = Id.ToString();
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Nonce = random.Next(10000, 100000);
            CreationDate = DateTime.Today;
            IsActive = true;
        }
    }
}
