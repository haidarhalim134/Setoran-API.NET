using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class refactortransaksipembayaranrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pembayaran_IdTransaksi",
                table: "Pembayaran");

            migrationBuilder.CreateIndex(
                name: "IX_Pembayaran_IdTransaksi",
                table: "Pembayaran",
                column: "IdTransaksi",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pembayaran_IdTransaksi",
                table: "Pembayaran");

            migrationBuilder.CreateIndex(
                name: "IX_Pembayaran_IdTransaksi",
                table: "Pembayaran",
                column: "IdTransaksi");
        }
    }
}
