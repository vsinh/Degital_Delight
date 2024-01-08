using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegitalDelight.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportInventory");

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "ReportDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalCost",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22b8a10b-6a3f-46a7-aa06-b8223a66347c", "AQAAAAIAAYagAAAAEJ0LAmmWvGpK862nTCqa1+/6Qa88n+T/Asq68rdfLX+vePCWdCRARn5HM+0fHrUZmA==", "c64582d7-719d-484a-b64b-8756f601c3ca" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportDetail_ReportId",
                table: "ReportDetail",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportDetail_Reports_ReportId",
                table: "ReportDetail",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportDetail_Reports_ReportId",
                table: "ReportDetail");

            migrationBuilder.DropIndex(
                name: "IX_ReportDetail_ReportId",
                table: "ReportDetail");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "ReportDetail");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Order");

            migrationBuilder.CreateTable(
                name: "ReportInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportInventory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d79f1df-6e2a-4e97-9a40-d7851b5570a4", "AQAAAAIAAYagAAAAECK3+WiFarCGqB4wYmtVRN9z/1Nr658qxrXCsUv7lWxI76BS4twmiERJo22gYOdJzg==", "782e2dac-bb54-42a8-8483-3d52bb6330d5" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportInventory_ProductId",
                table: "ReportInventory",
                column: "ProductId");
        }
    }
}
