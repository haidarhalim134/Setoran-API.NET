using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class fixdiskonrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diskon_Motor_MotorIdMotor",
                table: "Diskon");

            migrationBuilder.DropIndex(
                name: "IX_Diskon_MotorIdMotor",
                table: "Diskon");

            migrationBuilder.DropColumn(
                name: "MotorIdMotor",
                table: "Diskon");

            migrationBuilder.CreateIndex(
                name: "IX_Diskon_IdMotor",
                table: "Diskon",
                column: "IdMotor");

            migrationBuilder.AddForeignKey(
                name: "FK_Diskon_Motor_IdMotor",
                table: "Diskon",
                column: "IdMotor",
                principalTable: "Motor",
                principalColumn: "IdMotor",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diskon_Motor_IdMotor",
                table: "Diskon");

            migrationBuilder.DropIndex(
                name: "IX_Diskon_IdMotor",
                table: "Diskon");

            migrationBuilder.AddColumn<int>(
                name: "MotorIdMotor",
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
    }
}
