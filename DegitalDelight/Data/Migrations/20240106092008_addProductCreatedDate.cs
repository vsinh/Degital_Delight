using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegitalDelight.Data.Migrations
{
    /// <inheritdoc />
    public partial class addProductCreatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6dc8075b-77d8-4b9e-b36c-f72e2ff947a4", "AQAAAAIAAYagAAAAEM6brpqPMzfJASwjxwy18JskQGACm6j95+aPo/bgSvjRWnMk71avtfaxf8tclU1KYw==", "c5e4c702-ee65-4639-a098-a3303779e435" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f516ca9a-5226-4a75-9d95-32cb5426ed73", "AQAAAAIAAYagAAAAEL2uUcMRoVTVAI0sXW0mRwAfb/v4ah3tF9yTm7tXIwLJZTkshvwSnZ+AtUS4EmyRQA==", "d4855ec3-9990-4b5c-a5fd-b71b105486a2" });
        }
    }
}
