using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class stringtoenumsforsomemodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Transaksi.Status
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transaksi");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transaksi",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Pembayaran.StatusPembayaran
            migrationBuilder.DropColumn(
                name: "StatusPembayaran",
                table: "Pembayaran");

            migrationBuilder.AddColumn<int>(
                name: "StatusPembayaran",
                table: "Pembayaran",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Pembayaran.MetodePembayaran
            migrationBuilder.DropColumn(
                name: "MetodePembayaran",
                table: "Pembayaran");

            migrationBuilder.AddColumn<int>(
                name: "MetodePembayaran",
                table: "Pembayaran",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Motor.Transmisi
            migrationBuilder.DropColumn(
                name: "Transmisi",
                table: "Motor");

            migrationBuilder.AddColumn<int>(
                name: "Transmisi",
                table: "Motor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Motor.StatusMotor
            migrationBuilder.DropColumn(
                name: "StatusMotor",
                table: "Motor");

            migrationBuilder.AddColumn<int>(
                name: "StatusMotor",
                table: "Motor",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Transaksi",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "StatusPembayaran",
                table: "Pembayaran",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "MetodePembayaran",
                table: "Pembayaran",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Transmisi",
                table: "Motor",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "StatusMotor",
                table: "Motor",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
