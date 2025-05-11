using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class hargahariantodecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        // Drop the old column
        migrationBuilder.DropColumn(
            name: "HargaHarian",
            table: "Motor");

        // Re-add it with the new type
        migrationBuilder.AddColumn<decimal>(
            name: "HargaHarian",
            table: "Motor",
            type: "numeric(18,2)",
            nullable: false,
            defaultValue: 0m); // adjust default as needed
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the altered column
            migrationBuilder.DropColumn(
                name: "HargaHarian",
                table: "Motor");

            // Re-add the original column type
            migrationBuilder.AddColumn<decimal>(
                name: "HargaHarian",
                table: "Motor",
                type: "numeric",
                nullable: false,
                defaultValue: 0m); // match whatever the original default was
        }
    }
}
