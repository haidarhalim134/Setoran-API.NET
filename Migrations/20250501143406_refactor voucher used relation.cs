using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class refactorvoucherusedrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsed_Pelanggan_IdPelanggan",
                table: "VoucherUsed");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsed_Voucher_IdVoucher",
                table: "VoucherUsed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherUsed",
                table: "VoucherUsed");

            migrationBuilder.DropIndex(
                name: "IX_VoucherUsed_IdPelanggan",
                table: "VoucherUsed");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VoucherUsed");

            migrationBuilder.RenameColumn(
                name: "IdVoucher",
                table: "VoucherUsed",
                newName: "VoucherIdVoucher");

            migrationBuilder.RenameColumn(
                name: "IdPelanggan",
                table: "VoucherUsed",
                newName: "PelangganIdPelanggan");

            migrationBuilder.RenameIndex(
                name: "IX_VoucherUsed_IdVoucher",
                table: "VoucherUsed",
                newName: "IX_VoucherUsed_VoucherIdVoucher");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TanggalPembayaran",
                table: "Pembayaran",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherUsed",
                table: "VoucherUsed",
                columns: new[] { "PelangganIdPelanggan", "VoucherIdVoucher" });

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsed_Pelanggan_PelangganIdPelanggan",
                table: "VoucherUsed",
                column: "PelangganIdPelanggan",
                principalTable: "Pelanggan",
                principalColumn: "IdPelanggan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsed_Voucher_VoucherIdVoucher",
                table: "VoucherUsed",
                column: "VoucherIdVoucher",
                principalTable: "Voucher",
                principalColumn: "IdVoucher",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsed_Pelanggan_PelangganIdPelanggan",
                table: "VoucherUsed");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsed_Voucher_VoucherIdVoucher",
                table: "VoucherUsed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherUsed",
                table: "VoucherUsed");

            migrationBuilder.RenameColumn(
                name: "VoucherIdVoucher",
                table: "VoucherUsed",
                newName: "IdVoucher");

            migrationBuilder.RenameColumn(
                name: "PelangganIdPelanggan",
                table: "VoucherUsed",
                newName: "IdPelanggan");

            migrationBuilder.RenameIndex(
                name: "IX_VoucherUsed_VoucherIdVoucher",
                table: "VoucherUsed",
                newName: "IX_VoucherUsed_IdVoucher");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "VoucherUsed",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TanggalPembayaran",
                table: "Pembayaran",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherUsed",
                table: "VoucherUsed",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUsed_IdPelanggan",
                table: "VoucherUsed",
                column: "IdPelanggan");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsed_Pelanggan_IdPelanggan",
                table: "VoucherUsed",
                column: "IdPelanggan",
                principalTable: "Pelanggan",
                principalColumn: "IdPelanggan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsed_Voucher_IdVoucher",
                table: "VoucherUsed",
                column: "IdVoucher",
                principalTable: "Voucher",
                principalColumn: "IdVoucher",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
