using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DappAPI.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PublicAddress = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nonce = table.Column<long>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Capitals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Asset = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Capitals_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("05211189-8430-489b-909f-1243a2b78e97"), "40f3da60-cc40-414d-891d-ce62c5ff7499", "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("ce6fbecf-e6b2-4852-8371-434c8e279cac"), "f2fea001-b51b-4f8e-8a4d-b433c0e47aa3", "staff", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("40b9034b-b896-412b-8bf4-ccdc7f1d742d"), 0, "Long An", "d1417119-0c2f-461e-bf7f-287d7a68cdde", new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), "minhtan@gmail.com", false, "Nguyễn Minh Tân", false, null, 18865L, null, null, null, "0123456789", false, "0x7e576E3FFdFf96581f035B29B2E084299b72900c", "40b9034b-b896-412b-8bf4-ccdc7f1d742d", false, "0x7e576E3FFdFf96581f035B29B2E084299b72900c" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("6e7dc7c9-8b12-4d82-a980-43aad555f6b4"), 0, "Tiền Giang", "128d465e-0f64-4cdf-8d2c-f0fd7939dde5", new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), "trungthuan@gmail.com", false, "Đỗ Trung Thuận", false, null, 42147L, null, null, null, "0123456789", false, "0x21cE7DdE449766dF2855392D5cCf3Fe0A0728956", "6e7dc7c9-8b12-4d82-a980-43aad555f6b4", false, "0x21cE7DdE449766dF2855392D5cCf3Fe0A0728956" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("8a7f9c38-6c1d-4c6e-8ce9-277f1df30a47"), 0, "Đồng Tháp", "3141262d-f8d6-457c-a35e-fa92ef182033", new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), "thanhdat@gmail.com", false, "Võ Thành Đạt", false, null, 20455L, null, null, null, "0123456789", false, "0xA5346041f7663aA9868CF17868De08B114D6D6e9", "8a7f9c38-6c1d-4c6e-8ce9-277f1df30a47", false, "0xA5346041f7663aA9868CF17868De08B114D6D6e9" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("cff10220-1d6f-459c-ac8a-c4e49f5372cc"), 0, "Tp Hồ Chí Minh", "e8aba088-06e9-4728-a5fe-28865c3533ce", new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), "thituyet@gmail.com", false, "Nguyễn Thị Tuyết", false, null, 44180L, null, null, null, "0123456789", false, "0x6C328b00c3DE595e129f7e5156cBc911bf0a4f0f", "cff10220-1d6f-459c-ac8a-c4e49f5372cc", false, "0x6C328b00c3DE595e129f7e5156cBc911bf0a4f0f" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("5ca35383-e8d0-4c7e-9d20-5980131ee42d"), 0, "Bình Phước", "6a53c834-4cd1-4497-ba75-ffc2690f2e56", new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), "vantrung@gmail.com", false, "Trần Văn Trung", false, null, 50797L, null, null, null, "0123456789", false, "0x7d30398b31d20285Ba473e918DD3aCa63fa5621D", "5ca35383-e8d0-4c7e-9d20-5980131ee42d", false, "0x7d30398b31d20285Ba473e918DD3aCa63fa5621D" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e32b6c81-631b-407d-a2cb-f29c74a87bfd"), 0, "Bến Tre", "380333e5-1fca-4714-9b65-971a05ffa969", new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), "thanhtuyen@gmail.com", false, "Nguyễn Thị Thanh Tuyền", false, null, 67357L, null, null, null, "0123456789", false, "0x7686d4E7238F18C43a3F5D9004c5B9913EC094f6", "e32b6c81-631b-407d-a2cb-f29c74a87bfd", false, "0x7686d4E7238F18C43a3F5D9004c5B9913EC094f6" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("d3b5a4f3-ad29-4c36-aafd-de4a2e68d0b1"), 0, "Tp Hồ Chí Minh", "11888aac-45c1-4b8c-8f44-7485934f6e12", new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), "minhnhat@gmail.com", false, "Trần Minh Nhật", false, null, 46845L, null, null, null, "0123456789", false, "0xa30bB9f78044F8E304dB5bdb8F888780722635e5", "d3b5a4f3-ad29-4c36-aafd-de4a2e68d0b1", false, "0xa30bB9f78044F8E304dB5bdb8F888780722635e5" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("8f1d72c9-f533-43ae-a981-70e3fee41908"), 0, "Hà Nội", "a50c2fba-4e30-4b0d-8d78-dd2363123f68", new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), "nhatlinh@gmail.com", false, "Ngô Nhật Linh", false, null, 65159L, null, null, null, "0123456789", false, "0x25cc8c93bbFDf2D544f2F28FB9fb6fdC8be93019", "8f1d72c9-f533-43ae-a981-70e3fee41908", false, "0x25cc8c93bbFDf2D544f2F28FB9fb6fdC8be93019" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("89735130-eb06-4f09-a02e-a647a44431f2"), 0, "Cần Thơ", "ac453507-09ec-4115-a91c-1e6c75de2dbe", new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), "thuhien@gmail.com", false, "Nguyễn Thị Thu Hiền", false, null, 35095L, null, null, null, "0123456789", false, "0xEE7E6221739929881EF431692788D68ba852F788", "89735130-eb06-4f09-a02e-a647a44431f2", false, "0xEE7E6221739929881EF431692788D68ba852F788" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("c9548d10-9ee3-43dc-ae67-ec2fe24e77b9"), 0, "Vũng Tàu", "67dec69d-584e-4b5e-818e-298d8ffeb350", new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), "thanhhien@gmail.com", false, "Lê Thanh Hiển", false, null, 91919L, null, null, null, "0123456789", false, "0xBf581651E6Fd1681aA0F42e5Fc0aC8a288237E8d", "c9548d10-9ee3-43dc-ae67-ec2fe24e77b9", false, "0xBf581651E6Fd1681aA0F42e5Fc0aC8a288237E8d" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ce6fbecf-e6b2-4852-8371-434c8e279cac"), new Guid("c9548d10-9ee3-43dc-ae67-ec2fe24e77b9") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("05211189-8430-489b-909f-1243a2b78e97"), new Guid("40b9034b-b896-412b-8bf4-ccdc7f1d742d") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("05211189-8430-489b-909f-1243a2b78e97"), new Guid("6e7dc7c9-8b12-4d82-a980-43aad555f6b4") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("05211189-8430-489b-909f-1243a2b78e97"), new Guid("8a7f9c38-6c1d-4c6e-8ce9-277f1df30a47") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ce6fbecf-e6b2-4852-8371-434c8e279cac"), new Guid("cff10220-1d6f-459c-ac8a-c4e49f5372cc") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ce6fbecf-e6b2-4852-8371-434c8e279cac"), new Guid("89735130-eb06-4f09-a02e-a647a44431f2") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ce6fbecf-e6b2-4852-8371-434c8e279cac"), new Guid("e32b6c81-631b-407d-a2cb-f29c74a87bfd") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ce6fbecf-e6b2-4852-8371-434c8e279cac"), new Guid("d3b5a4f3-ad29-4c36-aafd-de4a2e68d0b1") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ce6fbecf-e6b2-4852-8371-434c8e279cac"), new Guid("8f1d72c9-f533-43ae-a981-70e3fee41908") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ce6fbecf-e6b2-4852-8371-434c8e279cac"), new Guid("5ca35383-e8d0-4c7e-9d20-5980131ee42d") });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 47L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("d3b5a4f3-ad29-4c36-aafd-de4a2e68d0b1"), "Đây là mô tả về khoản vốn số 47, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 47", 1, 167790000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 3L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8f1d72c9-f533-43ae-a981-70e3fee41908"), "Đây là mô tả về khoản vốn số 3, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 3", 0, 20490000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 8L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8f1d72c9-f533-43ae-a981-70e3fee41908"), "Đây là mô tả về khoản vốn số 8, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 8", 0, 70480000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 13L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8f1d72c9-f533-43ae-a981-70e3fee41908"), "Đây là mô tả về khoản vốn số 13, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 13", 1, 7280000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 24L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8f1d72c9-f533-43ae-a981-70e3fee41908"), "Đây là mô tả về khoản vốn số 24, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 24", 0, 32160000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 26L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8f1d72c9-f533-43ae-a981-70e3fee41908"), "Đây là mô tả về khoản vốn số 26, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 26", 1, 120380000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 46L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8f1d72c9-f533-43ae-a981-70e3fee41908"), "Đây là mô tả về khoản vốn số 46, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 46", 0, 442060000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 4L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("89735130-eb06-4f09-a02e-a647a44431f2"), "Đây là mô tả về khoản vốn số 4, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 4", 1, 600000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 14L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("89735130-eb06-4f09-a02e-a647a44431f2"), "Đây là mô tả về khoản vốn số 14, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 14", 0, 14980000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 23L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("89735130-eb06-4f09-a02e-a647a44431f2"), "Đây là mô tả về khoản vốn số 23, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 23", 1, 116380000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 42L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("89735130-eb06-4f09-a02e-a647a44431f2"), "Đây là mô tả về khoản vốn số 42, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 42", 1, 34860000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 45L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("89735130-eb06-4f09-a02e-a647a44431f2"), "Đây là mô tả về khoản vốn số 45, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 45", 1, 192600000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 44L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("d3b5a4f3-ad29-4c36-aafd-de4a2e68d0b1"), "Đây là mô tả về khoản vốn số 44, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 44", 1, 350240000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 16L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c9548d10-9ee3-43dc-ae67-ec2fe24e77b9"), "Đây là mô tả về khoản vốn số 16, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 16", 0, 29440000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 21L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c9548d10-9ee3-43dc-ae67-ec2fe24e77b9"), "Đây là mô tả về khoản vốn số 21, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 21", 1, 200340000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 31L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c9548d10-9ee3-43dc-ae67-ec2fe24e77b9"), "Đây là mô tả về khoản vốn số 31, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 31", 1, 13950000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 37L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c9548d10-9ee3-43dc-ae67-ec2fe24e77b9"), "Đây là mô tả về khoản vốn số 37, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 37", 1, 174270000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 41L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c9548d10-9ee3-43dc-ae67-ec2fe24e77b9"), "Đây là mô tả về khoản vốn số 41, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 41", 1, 368180000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 19L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("89735130-eb06-4f09-a02e-a647a44431f2"), "Đây là mô tả về khoản vốn số 19, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 19", 1, 89870000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 11L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c9548d10-9ee3-43dc-ae67-ec2fe24e77b9"), "Đây là mô tả về khoản vốn số 11, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 11", 0, 48620000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 39L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("d3b5a4f3-ad29-4c36-aafd-de4a2e68d0b1"), "Đây là mô tả về khoản vốn số 39, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 39", 0, 220740000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 29L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("d3b5a4f3-ad29-4c36-aafd-de4a2e68d0b1"), "Đây là mô tả về khoản vốn số 29, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 29", 0, 283910000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 15L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("40b9034b-b896-412b-8bf4-ccdc7f1d742d"), "Đây là mô tả về khoản vốn số 15, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 15", 0, 143250000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 30L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("40b9034b-b896-412b-8bf4-ccdc7f1d742d"), "Đây là mô tả về khoản vốn số 30, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 30", 1, 42300000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 35L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("40b9034b-b896-412b-8bf4-ccdc7f1d742d"), "Đây là mô tả về khoản vốn số 35, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 35", 1, 289450000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 36L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("40b9034b-b896-412b-8bf4-ccdc7f1d742d"), "Đây là mô tả về khoản vốn số 36, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 36", 0, 46800000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 38L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("40b9034b-b896-412b-8bf4-ccdc7f1d742d"), "Đây là mô tả về khoản vốn số 38, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 38", 1, 33440000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 49L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("40b9034b-b896-412b-8bf4-ccdc7f1d742d"), "Đây là mô tả về khoản vốn số 49, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 49", 0, 439040000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 1L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("6e7dc7c9-8b12-4d82-a980-43aad555f6b4"), "Đây là mô tả về khoản vốn số 1, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 1", 1, 3720000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 17L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("6e7dc7c9-8b12-4d82-a980-43aad555f6b4"), "Đây là mô tả về khoản vốn số 17, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 17", 0, 87210000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 33L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("6e7dc7c9-8b12-4d82-a980-43aad555f6b4"), "Đây là mô tả về khoản vốn số 33, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 33", 0, 170610000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 48L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("6e7dc7c9-8b12-4d82-a980-43aad555f6b4"), "Đây là mô tả về khoản vốn số 48, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 48", 1, 414720000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 50L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("6e7dc7c9-8b12-4d82-a980-43aad555f6b4"), "Đây là mô tả về khoản vốn số 50, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 50", 1, 348500000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 2L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8a7f9c38-6c1d-4c6e-8ce9-277f1df30a47"), "Đây là mô tả về khoản vốn số 2, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 2", 0, 9520000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 34L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("d3b5a4f3-ad29-4c36-aafd-de4a2e68d0b1"), "Đây là mô tả về khoản vốn số 34, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 34", 1, 5440000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 20L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8a7f9c38-6c1d-4c6e-8ce9-277f1df30a47"), "Đây là mô tả về khoản vốn số 20, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 20", 0, 77200000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 27L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8a7f9c38-6c1d-4c6e-8ce9-277f1df30a47"), "Đây là mô tả về khoản vốn số 27, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 27", 0, 93150000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 32L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8a7f9c38-6c1d-4c6e-8ce9-277f1df30a47"), "Đây là mô tả về khoản vốn số 32, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 32", 0, 255680000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 25L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cff10220-1d6f-459c-ac8a-c4e49f5372cc"), "Đây là mô tả về khoản vốn số 25, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 25", 0, 112250000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 43L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cff10220-1d6f-459c-ac8a-c4e49f5372cc"), "Đây là mô tả về khoản vốn số 43, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 43", 1, 198230000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 6L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ca35383-e8d0-4c7e-9d20-5980131ee42d"), "Đây là mô tả về khoản vốn số 6, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 6", 0, 39300000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 9L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ca35383-e8d0-4c7e-9d20-5980131ee42d"), "Đây là mô tả về khoản vốn số 9, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 9", 1, 67950000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 18L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ca35383-e8d0-4c7e-9d20-5980131ee42d"), "Đây là mô tả về khoản vốn số 18, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 18", 1, 160200000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 10L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("e32b6c81-631b-407d-a2cb-f29c74a87bfd"), "Đây là mô tả về khoản vốn số 10, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 10", 0, 22300000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 28L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("e32b6c81-631b-407d-a2cb-f29c74a87bfd"), "Đây là mô tả về khoản vốn số 28, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 28", 0, 244720000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 40L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("e32b6c81-631b-407d-a2cb-f29c74a87bfd"), "Đây là mô tả về khoản vốn số 40, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 40", 1, 265600000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 7L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("d3b5a4f3-ad29-4c36-aafd-de4a2e68d0b1"), "Đây là mô tả về khoản vốn số 7, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 7", 0, 49560000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 12L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("d3b5a4f3-ad29-4c36-aafd-de4a2e68d0b1"), "Đây là mô tả về khoản vốn số 12, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 12", 0, 117720000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 22L, 0, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8a7f9c38-6c1d-4c6e-8ce9-277f1df30a47"), "Đây là mô tả về khoản vốn số 22, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 22", 1, 45980000.0 });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 5L, 1, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("40b9034b-b896-412b-8bf4-ccdc7f1d742d"), "Đây là mô tả về khoản vốn số 5, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 5", 0, 49750000.0 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Capitals_CreatorId",
                table: "Capitals",
                column: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Capitals");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
