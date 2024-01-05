using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegitalDelight.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Discriminator", "Email", "EmailConfirmed", "ImagePath", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "f516ca9a-5226-4a75-9d95-32cb5426ed73", null, "User", null, false, null, false, false, null, null, "ADMIN@ADMIN", "AQAAAAIAAYagAAAAEL2uUcMRoVTVAI0sXW0mRwAfb/v4ah3tF9yTm7tXIwLJZTkshvwSnZ+AtUS4EmyRQA==", null, false, "d4855ec3-9990-4b5c-a5fd-b71b105486a2", false, "admin@admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "9d6bd7fd-9d1e-415d-994c-fd1e7e0f32ee", "IdentityUser", null, false, false, null, null, "ADMIN@ADMIN", "AQAAAAIAAYagAAAAEFPnbC79Jf7pCf9PrZyN2ZtGDMSsGmUYAoqxCXdmJF2q2fWV0cV4/kSVHoqQLch9ww==", null, false, "36b0be93-f865-4e74-b50a-5a943443c4ec", false, "admin@admin" });
        }
    }
}
