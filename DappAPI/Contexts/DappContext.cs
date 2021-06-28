using DappAPI.Extensions.Enums;
using DappAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DappAPI.Contexts
{
    public class DappContext : IdentityDbContext<DappUser, UserRole, Guid>
    {
        public DappContext(DbContextOptions<DappContext> options) : base(options)
        {

        }
        public void a()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public DbSet<DappUser> AppUsers { get; set; }
        public DbSet<Capital> Capitals { get; set; }
    }
}
