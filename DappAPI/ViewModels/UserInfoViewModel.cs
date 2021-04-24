﻿using System;

namespace DappAPI.ViewModels
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }
        public string PublicAddress { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
