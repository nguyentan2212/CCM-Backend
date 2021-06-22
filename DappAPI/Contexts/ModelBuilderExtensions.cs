using DappAPI.Extensions.Enums;
using DappAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DappAPI.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            UserRole admin = new UserRole()
            {
                Id = Guid.NewGuid(),
                Name = "admin",
            };
            UserRole staff = new UserRole()
            {
                Id = Guid.NewGuid(),
                Name = "staff",
            };
            modelBuilder.Entity<UserRole>().HasData(admin, staff);

            Random random = new Random();

            DappUser[] users = {
                new DappUser("0x7e576E3FFdFf96581f035B29B2E084299b72900c", "Nguyễn Minh Tân", "minhtan@gmail.com", "0123456789", "Long An"),
                new DappUser("0x21cE7DdE449766dF2855392D5cCf3Fe0A0728956", "Đỗ Trung Thuận", "trungthuan@gmail.com", "0123456789", "Tiền Giang"),
                new DappUser("0xA5346041f7663aA9868CF17868De08B114D6D6e9", "Võ Thành Đạt", "thanhdat@gmail.com", "0123456789", "Đồng Tháp"),
                new DappUser("0x6C328b00c3DE595e129f7e5156cBc911bf0a4f0f", "Nguyễn Thị Tuyết", "thituyet@gmail.com", "0123456789", "Tp Hồ Chí Minh"),               
                new DappUser("0x7d30398b31d20285Ba473e918DD3aCa63fa5621D", "Trần Văn Trung", "vantrung@gmail.com", "0123456789", "Bình Phước"),
                new DappUser("0x7686d4E7238F18C43a3F5D9004c5B9913EC094f6", "Nguyễn Thị Thanh Tuyền", "thanhtuyen@gmail.com", "0123456789", "Bến Tre"),
                new DappUser("0xa30bB9f78044F8E304dB5bdb8F888780722635e5", "Trần Minh Nhật", "minhnhat@gmail.com", "0123456789", "Tp Hồ Chí Minh"),             
                new DappUser("0x25cc8c93bbFDf2D544f2F28FB9fb6fdC8be93019", "Ngô Nhật Linh", "nhatlinh@gmail.com", "0123456789", "Hà Nội"),
                new DappUser("0xEE7E6221739929881EF431692788D68ba852F788", "Nguyễn Thị Thu Hiền", "thuhien@gmail.com", "0123456789", "Cần Thơ"),
                new DappUser("0xBf581651E6Fd1681aA0F42e5Fc0aC8a288237E8d", "Lê Thanh Hiển", "thanhhien@gmail.com", "0123456789", "Vũng Tàu")
            };
            modelBuilder.Entity<DappUser>().HasData(users);

            List<IdentityUserRole<Guid>> userRoles = new List<IdentityUserRole<Guid>>();
            for (int i = 0; i < 3; i++)
            {
                userRoles.Add(new IdentityUserRole<Guid>()
                {
                    RoleId = admin.Id,
                    UserId = users[i].Id
                });
            }
            for (int i = 3; i < 10; i++)
            {
                userRoles.Add(new IdentityUserRole<Guid>()
                {
                    RoleId = staff.Id,
                    UserId = users[i].Id
                });
            }
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(userRoles);

            List<Capital> capitals = new List<Capital>();
            for (int i = 1; i <= 50; i++)
            {
                string title = $"Khoản vốn số {i}";
                string description = $"Đây là mô tả về khoản vốn số {i}, khoản vốn này được tạo bởi code-behind.";
                double value = i * random.Next(1, 1000) * 10000;
                int asset = random.Next(2);
                int type = random.Next(2);
                int user = random.Next(10);
                Capital capital = new Capital(i, title, description, value, (AssetType)asset, (CapitalType)type);
                capital.CreatorId = users[user].Id;
                capitals.Add(capital);
            }
            modelBuilder.Entity<Capital>().HasData(capitals);
        }
    }
}