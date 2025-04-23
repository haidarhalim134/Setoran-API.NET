using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class voucherrenameidaddstatuspercentcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Potongan",
                table: "Voucher");

            migrationBuilder.RenameColumn(
                name: "Tipe",
                table: "Voucher",
                newName: "NamaVoucher");

            migrationBuilder.RenameColumn(
                name: "Kode",
                table: "Voucher",
                newName: "KodeVoucher");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Voucher",
                newName: "IdVoucher");

            migrationBuilder.AddColumn<int>(
                name: "PersenVoucher",
                table: "Voucher",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusVoucher",
                table: "Voucher",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersenVoucher",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "StatusVoucher",
                table: "Voucher");

            migrationBuilder.RenameColumn(
                name: "NamaVoucher",
                table: "Voucher",
                newName: "Tipe");

            migrationBuilder.RenameColumn(
                name: "KodeVoucher",
                table: "Voucher",
                newName: "Kode");

            migrationBuilder.RenameColumn(
                name: "IdVoucher",
                table: "Voucher",
                newName: "Id");

            migrationBuilder.AddColumn<decimal>(
                name: "Potongan",
                table: "Voucher",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
