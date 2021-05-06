using DappAPI.Extensions.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DappAPI.Models
{
    public class Capital
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public CapitalType Type { get; set; }
        [Required]
        public CapitalStatus Status { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        public DappUser Creator { get; set; }
        public DappUser Approver { get; set; }

    }
}
