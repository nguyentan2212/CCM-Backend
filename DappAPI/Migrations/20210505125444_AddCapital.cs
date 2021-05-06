using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DappAPI.Migrations
{
    public partial class AddCapital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8bf2d9f5-9326-4080-a0cc-ec80e86eaa7f"));

            migrationBuilder.CreateTable(
                name: "Capitals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ApproverId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Capitals_AspNetUsers_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Capitals_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Birthdate", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("c0505408-4301-4d2c-a9c2-dbd671608622"), 0, "Ho Chi Minh City", new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Local), "de461ffa-d6df-45ca-b2aa-13d8ae515b3c", new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Local), null, false, "Nguyen Minh Tan", false, null, 31754L, null, null, null, null, false, "0x7e576E3FFdFf96581f035B29B2E084299b72900c", null, false, null });

            migrationBuilder.CreateIndex(
                name: "IX_Capitals_ApproverId",
                table: "Capitals",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_Capitals_CreatorId",
                table: "Capitals",
                column: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Capitals");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c0505408-4301-4d2c-a9c2-dbd671608622"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Birthdate", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Nonce", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublicAddress", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("8bf2d9f5-9326-4080-a0cc-ec80e86eaa7f"), 0, "Ho Chi Minh City", new DateTime(2021, 4, 24, 0, 0, 0, 0, DateTimeKind.Local), "d7c134ca-574d-495f-b780-d6f709e73351", new DateTime(2021, 4, 24, 0, 0, 0, 0, DateTimeKind.Local), null, false, "Nguyen Minh Tan", false, null, 34527L, null, null, null, null, false, "0x7e576e3ffdff96581f035b29b2e084299b72900c", null, false, null });
        }
    }
}
