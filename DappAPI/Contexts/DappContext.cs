using DappAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DappAPI.Contexts
{
    public class DappContext : IdentityDbContext<DappUser, IdentityRole<Guid>, Guid>
    {
        public DappContext(DbContextOptions<DappContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public DbSet<DappUser> AppUsers { get; set; }
    }
}
