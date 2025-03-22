using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class RelasiPelangganPengguna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pelanggan_IdPengguna",
                table: "Pelanggan",
                column: "IdPengguna",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pelanggan_AspNetUsers_IdPengguna",
                table: "Pelanggan",
                column: "IdPengguna",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelanggan_AspNetUsers_IdPengguna",
                table: "Pelanggan");

            migrationBuilder.DropIndex(
                name: "IX_Pelanggan_IdPengguna",
                table: "Pelanggan");
        }
    }
}
