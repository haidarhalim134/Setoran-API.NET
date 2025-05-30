using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class adddecimaldecoratordimodeltransaksi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalHargaTemp",
                table: "Transaksi",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE ""Transaksi""
                SET ""TotalHargaTemp"" = 
                    CASE 
                        WHEN trim(""TotalHarga"") ~ '^\d+(\.\d+)?$' THEN CAST(""TotalHarga"" AS numeric(18,2))
                        ELSE 0
                    END
            ");

            migrationBuilder.DropColumn(
                name: "TotalHarga",
                table: "Transaksi");

            migrationBuilder.RenameColumn(
                name: "TotalHargaTemp",
                table: "Transaksi",
                newName: "TotalHarga");

        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TotalHarga",
                table: "Transaksi",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");
            }
    }
}
