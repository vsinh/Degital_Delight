using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegitalDelight.Data.Migrations
{
    /// <inheritdoc />
    public partial class addDiscountQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db4ed162-b232-4038-b6d8-cfacafe851ab", "AQAAAAIAAYagAAAAEOxwB3yQ3SVrgIre6uummIFaGE/IPSlcfQ4XhfQUQ+TZzySJDNbQN/4Y1RGD0RAdtQ==", "3cb7bdf8-0ae1-4578-805f-ddfa3f804e3c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Discounts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6dc8075b-77d8-4b9e-b36c-f72e2ff947a4", "AQAAAAIAAYagAAAAEM6brpqPMzfJASwjxwy18JskQGACm6j95+aPo/bgSvjRWnMk71avtfaxf8tclU1KYw==", "c5e4c702-ee65-4639-a098-a3303779e435" });
        }
    }
}
