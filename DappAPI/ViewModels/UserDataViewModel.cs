using System;
using System.Collections.Generic;
using DappAPI.ViewModels;

namespace DappAPI.ViewModels
{
    public class UserDataViewModel
    {
        public string Id { get; set; }
        public string PublicAddress { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<CapitalDataViewModel> CreatedCapitals { get; set; }
        public List<CapitalDataViewModel> ApprovedCapitals { get; set; }
    }
}
