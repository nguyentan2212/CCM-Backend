using DappAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DappAPI.Contexts
{
    public class DappContext : IdentityDbContext<DappUser, UserRole, Guid>
    {
        public DappContext(DbContextOptions<DappContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DappUser>()
                .HasMany(a => a.CreatedCapitals)
                .WithOne(b => b.Creator)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DappUser>()
                .HasMany(a => a.ApprovedCapitals)
                .WithOne(b => b.Approver)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Seed();
        }

        public DbSet<DappUser> AppUsers { get; set; }
        public DbSet<Capital> Capitals { get; set; }
    }
}
