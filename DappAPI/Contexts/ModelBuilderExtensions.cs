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
                    Id = Guid.NewGuid(),
                    FullName = "Nguyen Minh Tan",
                    PublicAddress = "0x7e576e3ffdff96581f035b29b2e084299b72900c",
                    Nonce = random.Next(10000,100000),
                    BirthDate = DateTime.Today,
                    CreationDate = DateTime.Today
                }
                );
        }
    }
}
