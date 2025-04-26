using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class addnotifikasi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifikasi",
                columns: table => new
                {
                    IdNotifikasi = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPengguna = table.Column<string>(type: "text", nullable: false),
                    Judul = table.Column<string>(type: "text", nullable: true),
                    Deskripsi = table.Column<string>(type: "text", nullable: true),
                    Navigasi = table.Column<int>(type: "integer", nullable: false),
                    DataNavigasi = table.Column<string>(type: "text", nullable: true),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikasi", x => x.IdNotifikasi);
                    table.ForeignKey(
                        name: "FK_Notifikasi_AspNetUsers_IdPengguna",
                        column: x => x.IdPengguna,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifikasi_IdPengguna",
                table: "Notifikasi",
                column: "IdPengguna");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifikasi");
        }
    }
}
