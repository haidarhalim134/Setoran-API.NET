using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class updatediskonimplementation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiskonAmount",
                table: "Motor");

            migrationBuilder.DropColumn(
                name: "DiskonPercentage",
                table: "Motor");

            migrationBuilder.DropColumn(
                name: "StatusPromo",
                table: "Diskon");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Diskon",
                newName: "IdDiskon");

            migrationBuilder.AddColumn<string>(
                name: "Deskripsi",
                table: "Diskon",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdMotor",
                table: "Diskon",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "JumlahDiskon",
                table: "Diskon",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MotorIdMotor",
                table: "Diskon",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusDiskon",
                table: "Diskon",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Diskon_MotorIdMotor",
                table: "Diskon",
                column: "MotorIdMotor");

            migrationBuilder.AddForeignKey(
                name: "FK_Diskon_Motor_MotorIdMotor",
                table: "Diskon",
                column: "MotorIdMotor",
                principalTable: "Motor",
                principalColumn: "IdMotor",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diskon_Motor_MotorIdMotor",
                table: "Diskon");

            migrationBuilder.DropIndex(
                name: "IX_Diskon_MotorIdMotor",
                table: "Diskon");

            migrationBuilder.DropColumn(
                name: "Deskripsi",
                table: "Diskon");

            migrationBuilder.DropColumn(
                name: "IdMotor",
                table: "Diskon");

            migrationBuilder.DropColumn(
                name: "JumlahDiskon",
                table: "Diskon");

            migrationBuilder.DropColumn(
                name: "MotorIdMotor",
                table: "Diskon");

            migrationBuilder.DropColumn(
                name: "StatusDiskon",
                table: "Diskon");

            migrationBuilder.RenameColumn(
                name: "IdDiskon",
                table: "Diskon",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "DiskonAmount",
                table: "Motor",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiskonPercentage",
                table: "Motor",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusPromo",
                table: "Diskon",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
