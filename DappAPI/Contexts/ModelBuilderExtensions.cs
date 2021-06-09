using DappAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DappAPI.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            Random random = new Random();

            modelBuilder.Entity<DappUser>().HasData(
                new DappUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FullName = "Nguyen Minh Tan",
                    PublicAddress = "0x7e576E3FFdFf96581f035B29B2E084299b72900c",
                    Nonce = random.Next(10000,100000),
                    CreationDate = DateTime.Today,
                    Address = "Ho Chi Minh City",
                    Email = "abc@.def.com",
                    PhoneNumber = "0123456789"
                }
                );
        }
    }
}
