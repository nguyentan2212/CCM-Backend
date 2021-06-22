using DappAPI.Extensions.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DappAPI.ViewModels
{
    public class CapitalDataViewModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public string Asset { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Status { get; set; }
        public string Creator { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
    }
}
