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
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
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
                name: "Utilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalMoney = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilities", x => x.Id);
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
                    Value = table.Column<long>(type: "INTEGER", nullable: false),
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
                values: new object[] { new Guid("676382df-01bb-4b2d-8855-fe82ec1caba4"), "763eb9c0-3644-4ab5-8894-a81deeaf869c", "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d4a7faf2-c2fc-4520-ac08-5db7eb8aa1c8"), "b7484871-464f-40fb-aac6-cec5b867112a", "staff", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("57b22cff-3a1c-47a2-8381-cfb56b91a637"), 0, "Long An", "69cc3896-f3b7-443f-b546-6f33c104be37", new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), "minhtan@gmail.com", false, "Nguyễn Minh Tân", true, false, null, 97188L, null, null, null, "0123456789", false, "0x7e576E3FFdFf96581f035B29B2E084299b72900c", "57b22cff-3a1c-47a2-8381-cfb56b91a637", false, "0x7e576E3FFdFf96581f035B29B2E084299b72900c" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("8528ddb0-c3f0-4577-b019-50219816304e"), 0, "Tiền Giang", "0a540f00-cff4-458f-bf76-74ea5401ea64", new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), "trungthuan@gmail.com", false, "Đỗ Trung Thuận", true, false, null, 32003L, null, null, null, "0123456789", false, "0x21cE7DdE449766dF2855392D5cCf3Fe0A0728956", "8528ddb0-c3f0-4577-b019-50219816304e", false, "0x21cE7DdE449766dF2855392D5cCf3Fe0A0728956" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("fb609d93-bb65-4571-9bb6-d156a7c0b5d2"), 0, "Đồng Tháp", "3d9f1cce-05dc-49ef-9fa0-5b4c3e2d30c3", new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), "thanhdat@gmail.com", false, "Võ Thành Đạt", true, false, null, 15749L, null, null, null, "0123456789", false, "0xA5346041f7663aA9868CF17868De08B114D6D6e9", "fb609d93-bb65-4571-9bb6-d156a7c0b5d2", false, "0xA5346041f7663aA9868CF17868De08B114D6D6e9" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("7b046999-1150-4914-932c-64980e7732e5"), 0, "Tp Hồ Chí Minh", "c53c6884-6f8e-4f38-ab57-87a2687818eb", new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), "thituyet@gmail.com", false, "Nguyễn Thị Tuyết", true, false, null, 66156L, null, null, null, "0123456789", false, "0x6C328b00c3DE595e129f7e5156cBc911bf0a4f0f", "7b046999-1150-4914-932c-64980e7732e5", false, "0x6C328b00c3DE595e129f7e5156cBc911bf0a4f0f" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("941cd15c-91e0-4ae4-b0c7-a31f9636432e"), 0, "Bình Phước", "8f03711c-cb2f-4282-aa1b-ccd5b430642f", new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), "vantrung@gmail.com", false, "Trần Văn Trung", true, false, null, 52875L, null, null, null, "0123456789", false, "0x7d30398b31d20285Ba473e918DD3aCa63fa5621D", "941cd15c-91e0-4ae4-b0c7-a31f9636432e", false, "0x7d30398b31d20285Ba473e918DD3aCa63fa5621D" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("5ee1d9f4-1fbb-4f32-a593-8b26b1fec323"), 0, "Bến Tre", "8b4b2fc9-5fd7-4591-b185-29cfd93142a3", new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), "thanhtuyen@gmail.com", false, "Nguyễn Thị Thanh Tuyền", true, false, null, 13545L, null, null, null, "0123456789", false, "0x7686d4E7238F18C43a3F5D9004c5B9913EC094f6", "5ee1d9f4-1fbb-4f32-a593-8b26b1fec323", false, "0x7686d4E7238F18C43a3F5D9004c5B9913EC094f6" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4673aa5a-c45a-48a5-b9f0-a842c5e91ef9"), 0, "Tp Hồ Chí Minh", "609ed9ea-5e31-4aa8-9232-173af22b5f54", new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), "minhnhat@gmail.com", false, "Trần Minh Nhật", true, false, null, 92102L, null, null, null, "0123456789", false, "0xa30bB9f78044F8E304dB5bdb8F888780722635e5", "4673aa5a-c45a-48a5-b9f0-a842c5e91ef9", false, "0xa30bB9f78044F8E304dB5bdb8F888780722635e5" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), 0, "Hà Nội", "3ca7748f-1822-44d6-8a66-3c73c77f4f12", new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), "nhatlinh@gmail.com", false, "Ngô Nhật Linh", true, false, null, 87125L, null, null, null, "0123456789", false, "0x25cc8c93bbFDf2D544f2F28FB9fb6fdC8be93019", "c8bc295e-e32c-4bc1-9a09-5ded9bd81e34", false, "0x25cc8c93bbFDf2D544f2F28FB9fb6fdC8be93019" });

            migrationBuilder.InsertData(
                table: "Utilities",
                columns: new[] { "Id", "TotalMoney" },
                values: new object[] { 1L, 7107050000L });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d4a7faf2-c2fc-4520-ac08-5db7eb8aa1c8"), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("676382df-01bb-4b2d-8855-fe82ec1caba4"), new Guid("57b22cff-3a1c-47a2-8381-cfb56b91a637") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("676382df-01bb-4b2d-8855-fe82ec1caba4"), new Guid("8528ddb0-c3f0-4577-b019-50219816304e") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d4a7faf2-c2fc-4520-ac08-5db7eb8aa1c8"), new Guid("fb609d93-bb65-4571-9bb6-d156a7c0b5d2") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d4a7faf2-c2fc-4520-ac08-5db7eb8aa1c8"), new Guid("4673aa5a-c45a-48a5-b9f0-a842c5e91ef9") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d4a7faf2-c2fc-4520-ac08-5db7eb8aa1c8"), new Guid("941cd15c-91e0-4ae4-b0c7-a31f9636432e") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d4a7faf2-c2fc-4520-ac08-5db7eb8aa1c8"), new Guid("5ee1d9f4-1fbb-4f32-a593-8b26b1fec323") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d4a7faf2-c2fc-4520-ac08-5db7eb8aa1c8"), new Guid("7b046999-1150-4914-932c-64980e7732e5") });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 14L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ee1d9f4-1fbb-4f32-a593-8b26b1fec323"), "Đây là mô tả về khoản vốn số 14, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 14", 0, 37940000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 17L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ee1d9f4-1fbb-4f32-a593-8b26b1fec323"), "Đây là mô tả về khoản vốn số 17, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 17", 1, 47090000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 19L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ee1d9f4-1fbb-4f32-a593-8b26b1fec323"), "Đây là mô tả về khoản vốn số 19, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 19", 1, 30780000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 26L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ee1d9f4-1fbb-4f32-a593-8b26b1fec323"), "Đây là mô tả về khoản vốn số 26, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 26", 1, 166140000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 39L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ee1d9f4-1fbb-4f32-a593-8b26b1fec323"), "Đây là mô tả về khoản vốn số 39, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 39", 0, 288600000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 49L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ee1d9f4-1fbb-4f32-a593-8b26b1fec323"), "Đây là mô tả về khoản vốn số 49, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 49", 1, 438060000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 6L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("4673aa5a-c45a-48a5-b9f0-a842c5e91ef9"), "Đây là mô tả về khoản vốn số 6, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 6", 1, 32040000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 16L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("4673aa5a-c45a-48a5-b9f0-a842c5e91ef9"), "Đây là mô tả về khoản vốn số 16, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 16", 1, 119840000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 23L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("4673aa5a-c45a-48a5-b9f0-a842c5e91ef9"), "Đây là mô tả về khoản vốn số 23, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 23", 0, 163760000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 18L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), "Đây là mô tả về khoản vốn số 18, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 18", 1, 128160000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 22L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), "Đây là mô tả về khoản vốn số 22, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 22", 0, 137060000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 11L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ee1d9f4-1fbb-4f32-a593-8b26b1fec323"), "Đây là mô tả về khoản vốn số 11, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 11", 1, 87010000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 30L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), "Đây là mô tả về khoản vốn số 30, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 30", 0, 155700000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 32L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), "Đây là mô tả về khoản vốn số 32, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 32", 1, 136960000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 36L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), "Đây là mô tả về khoản vốn số 36, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 36", 0, 20160000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 37L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), "Đây là mô tả về khoản vốn số 37, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 37", 1, 324120000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 38L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), "Đây là mô tả về khoản vốn số 38, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 38", 1, 178600000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 40L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), "Đây là mô tả về khoản vốn số 40, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 40", 0, 148800000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 48L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), "Đây là mô tả về khoản vốn số 48, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 48", 1, 210720000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 3L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), "Đây là mô tả về khoản vốn số 3, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 3", 1, 27810000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 25L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c8bc295e-e32c-4bc1-9a09-5ded9bd81e34"), "Đây là mô tả về khoản vốn số 25, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 25", 0, 90750000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 8L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ee1d9f4-1fbb-4f32-a593-8b26b1fec323"), "Đây là mô tả về khoản vốn số 8, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 8", 1, 17760000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 31L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("941cd15c-91e0-4ae4-b0c7-a31f9636432e"), "Đây là mô tả về khoản vốn số 31, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 31", 1, 142910000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 21L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("57b22cff-3a1c-47a2-8381-cfb56b91a637"), "Đây là mô tả về khoản vốn số 21, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 21", 1, 87990000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 28L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("57b22cff-3a1c-47a2-8381-cfb56b91a637"), "Đây là mô tả về khoản vốn số 28, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 28", 0, 136920000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 34L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("57b22cff-3a1c-47a2-8381-cfb56b91a637"), "Đây là mô tả về khoản vốn số 34, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 34", 1, 281180000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 42L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("57b22cff-3a1c-47a2-8381-cfb56b91a637"), "Đây là mô tả về khoản vốn số 42, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 42", 0, 199500000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 50L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("57b22cff-3a1c-47a2-8381-cfb56b91a637"), "Đây là mô tả về khoản vốn số 50, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 50", 0, 232500000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 4L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8528ddb0-c3f0-4577-b019-50219816304e"), "Đây là mô tả về khoản vốn số 4, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 4", 0, 34840000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 12L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8528ddb0-c3f0-4577-b019-50219816304e"), "Đây là mô tả về khoản vốn số 12, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 12", 0, 105600000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 15L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8528ddb0-c3f0-4577-b019-50219816304e"), "Đây là mô tả về khoản vốn số 15, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 15", 0, 42300000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 20L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8528ddb0-c3f0-4577-b019-50219816304e"), "Đây là mô tả về khoản vốn số 20, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 20", 1, 88000000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 27L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8528ddb0-c3f0-4577-b019-50219816304e"), "Đây là mô tả về khoản vốn số 27, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 27", 0, 160650000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 35L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8528ddb0-c3f0-4577-b019-50219816304e"), "Đây là mô tả về khoản vốn số 35, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 35", 1, 309750000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 41L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8528ddb0-c3f0-4577-b019-50219816304e"), "Đây là mô tả về khoản vốn số 41, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 41", 1, 226320000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 44L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8528ddb0-c3f0-4577-b019-50219816304e"), "Đây là mô tả về khoản vốn số 44, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 44", 1, 383680000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 46L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("8528ddb0-c3f0-4577-b019-50219816304e"), "Đây là mô tả về khoản vốn số 46, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 46", 0, 337180000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 7L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("fb609d93-bb65-4571-9bb6-d156a7c0b5d2"), "Đây là mô tả về khoản vốn số 7, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 7", 1, 54460000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 10L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("fb609d93-bb65-4571-9bb6-d156a7c0b5d2"), "Đây là mô tả về khoản vốn số 10, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 10", 1, 70400000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 24L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("fb609d93-bb65-4571-9bb6-d156a7c0b5d2"), "Đây là mô tả về khoản vốn số 24, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 24", 1, 20160000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 29L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("fb609d93-bb65-4571-9bb6-d156a7c0b5d2"), "Đây là mô tả về khoản vốn số 29, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 29", 1, 278110000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 43L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("fb609d93-bb65-4571-9bb6-d156a7c0b5d2"), "Đây là mô tả về khoản vốn số 43, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 43", 0, 218870000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 45L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("fb609d93-bb65-4571-9bb6-d156a7c0b5d2"), "Đây là mô tả về khoản vốn số 45, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 45", 1, 238500000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 47L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("fb609d93-bb65-4571-9bb6-d156a7c0b5d2"), "Đây là mô tả về khoản vốn số 47, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 47", 1, 219490000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 5L, 1, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("7b046999-1150-4914-932c-64980e7732e5"), "Đây là mô tả về khoản vốn số 5, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 5", 0, 20900000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 13L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("7b046999-1150-4914-932c-64980e7732e5"), "Đây là mô tả về khoản vốn số 13, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 13", 0, 50960000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 33L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("7b046999-1150-4914-932c-64980e7732e5"), "Đây là mô tả về khoản vốn số 33, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 33", 0, 149160000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 9L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("941cd15c-91e0-4ae4-b0c7-a31f9636432e"), "Đây là mô tả về khoản vốn số 9, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 9", 1, 5310000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 2L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("5ee1d9f4-1fbb-4f32-a593-8b26b1fec323"), "Đây là mô tả về khoản vốn số 2, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 2", 1, 14400000L });

            migrationBuilder.InsertData(
                table: "Capitals",
                columns: new[] { "Id", "Asset", "CreationDate", "CreatorId", "Description", "Status", "Title", "Type", "Value" },
                values: new object[] { 1L, 0, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("57b22cff-3a1c-47a2-8381-cfb56b91a637"), "Đây là mô tả về khoản vốn số 1, khoản vốn này được tạo bởi code-behind.", 0, "Khoản vốn số 1", 1, 9150000L });

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
                name: "Utilities");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
