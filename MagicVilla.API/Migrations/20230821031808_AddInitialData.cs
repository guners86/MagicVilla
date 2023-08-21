using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla.API.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "Cost", "CreationDate", "Details", "ImageUrl", "Name", "Ocupance", "SquareMeter", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Muy amena", 100.0, new DateTime(2023, 8, 20, 22, 18, 8, 782, DateTimeKind.Local).AddTicks(5188), "Detalle de la piscina", "", "Vista a la Piscina", 5, 300, new DateTime(2023, 8, 20, 22, 18, 8, 782, DateTimeKind.Local).AddTicks(5198) },
                    { 2, "Muy amena", 400.0, new DateTime(2023, 8, 20, 22, 18, 8, 782, DateTimeKind.Local).AddTicks(5201), "Detalle del mar", "", "Vista a al Mar", 10, 300, new DateTime(2023, 8, 20, 22, 18, 8, 782, DateTimeKind.Local).AddTicks(5201) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
