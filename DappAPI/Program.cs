using DappAPI.Contexts;
using DappAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace DappAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            try
            {
                var scope = host.Services.CreateScope();
                var ctx = scope.ServiceProvider.GetRequiredService<DappContext>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<DappUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();
                var admin = new UserRole() {  Name = "admin" };
                var staff = new UserRole() {  Name = "staff" };
                
                if (!ctx.Roles.Any())
                {                               
                    roleMgr.CreateAsync(admin).GetAwaiter().GetResult();
                    roleMgr.CreateAsync(staff).GetAwaiter().GetResult();
                    ctx.SaveChanges();
                }
                Random random = new Random();
                string publicAddress = "0x7e576E3FFdFf96581f035B29B2E084299b72900c";

                DappUser model = new DappUser()
                {
                    Id = Guid.NewGuid(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PublicAddress = publicAddress,
                    UserName = publicAddress,
                    FullName = "tan",
                    Address = "tan an",
                    Email = "tan@gmail.com",
                    PhoneNumber = "0123456789",
                    CreationDate = DateTime.Today,
                    Nonce = random.Next(10000, 100000),
                };
                ctx.AppUsers.Add(model);
                ctx.SaveChanges();
                var result = userMgr.AddToRoleAsync(model, "admin").GetAwaiter().GetResult();

                if (ctx.AppUsers.Count() < 2)
                {                   
                    for (int i = 0; i <= 50; i++)
                    {
                        model = new DappUser()
                        {
                            Id = Guid.NewGuid(),
                            SecurityStamp = Guid.NewGuid().ToString(),
                            PublicAddress = $"{publicAddress}{i}",
                            UserName = $"{publicAddress}{i}",
                            FullName = $"tan {i}",
                            Address = $"tan an {i}",
                            Email = $"tan{i}@gmail.com",
                            PhoneNumber = $"0123456789{i}",
                            CreationDate = DateTime.Today,
                            Nonce = random.Next(10000, 100000),
                        };
                        ctx.AppUsers.Add(model);
                        ctx.SaveChanges();
                        result = userMgr.AddToRoleAsync(model, "staff").GetAwaiter().GetResult();
                    }
                }               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
