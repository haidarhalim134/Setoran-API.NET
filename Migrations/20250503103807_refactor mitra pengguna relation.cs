using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class refactormitrapenggunarelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mitra_AspNetUsers_Id",
                table: "Mitra");

            migrationBuilder.DropIndex(
                name: "IX_Mitra_Id",
                table: "Mitra");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Mitra");

            migrationBuilder.AddColumn<string>(
                name: "IdPengguna",
                table: "Mitra",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Mitra_IdPengguna",
                table: "Mitra",
                column: "IdPengguna",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mitra_AspNetUsers_IdPengguna",
                table: "Mitra",
                column: "IdPengguna",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mitra_AspNetUsers_IdPengguna",
                table: "Mitra");

            migrationBuilder.DropIndex(
                name: "IX_Mitra_IdPengguna",
                table: "Mitra");

            migrationBuilder.DropColumn(
                name: "IdPengguna",
                table: "Mitra");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Mitra",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mitra_Id",
                table: "Mitra",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mitra_AspNetUsers_Id",
                table: "Mitra",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
