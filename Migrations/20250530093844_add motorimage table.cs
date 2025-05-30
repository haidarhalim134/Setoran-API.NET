using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class addmotorimagetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdMotorImage",
                table: "Motor",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MotorImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdMotor = table.Column<int>(type: "integer", nullable: false),
                    Front = table.Column<string>(type: "text", nullable: false),
                    Left = table.Column<string>(type: "text", nullable: false),
                    Right = table.Column<string>(type: "text", nullable: false),
                    Rear = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotorImage_Motor_IdMotor",
                        column: x => x.IdMotor,
                        principalTable: "Motor",
                        principalColumn: "IdMotor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motor_IdMotorImage",
                table: "Motor",
                column: "IdMotorImage");

            migrationBuilder.CreateIndex(
                name: "IX_MotorImage_IdMotor",
                table: "MotorImage",
                column: "IdMotor");

            migrationBuilder.AddForeignKey(
                name: "FK_Motor_MotorImage_IdMotorImage",
                table: "Motor",
                column: "IdMotorImage",
                principalTable: "MotorImage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motor_MotorImage_IdMotorImage",
                table: "Motor");

            migrationBuilder.DropTable(
                name: "MotorImage");

            migrationBuilder.DropIndex(
                name: "IX_Motor_IdMotorImage",
                table: "Motor");

            migrationBuilder.DropColumn(
                name: "IdMotorImage",
                table: "Motor");
        }
    }
}
