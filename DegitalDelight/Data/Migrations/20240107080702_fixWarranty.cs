using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegitalDelight.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixWarranty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Warrantys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d79f1df-6e2a-4e97-9a40-d7851b5570a4", "AQAAAAIAAYagAAAAECK3+WiFarCGqB4wYmtVRN9z/1Nr658qxrXCsUv7lWxI76BS4twmiERJo22gYOdJzg==", "782e2dac-bb54-42a8-8483-3d52bb6330d5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Warrantys");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db4ed162-b232-4038-b6d8-cfacafe851ab", "AQAAAAIAAYagAAAAEOxwB3yQ3SVrgIre6uummIFaGE/IPSlcfQ4XhfQUQ+TZzySJDNbQN/4Y1RGD0RAdtQ==", "3cb7bdf8-0ae1-4578-805f-ddfa3f804e3c" });
        }
    }
}
