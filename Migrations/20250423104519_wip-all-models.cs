using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Setoran_API.NET.Migrations
{
    /// <inheritdoc />
    public partial class wipallmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Nama = table.Column<string>(type: "text", nullable: true),
                    TanggalLahir = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NomorTelepon = table.Column<string>(type: "text", nullable: true),
                    Umur = table.Column<int>(type: "integer", nullable: true),
                    NomorKTP = table.Column<string>(type: "text", nullable: true),
                    Alamat = table.Column<string>(type: "text", nullable: true),
                    IdGambar = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diskon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nama = table.Column<string>(type: "text", nullable: false),
                    StatusPromo = table.Column<string>(type: "text", nullable: false),
                    TanggalMulai = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TanggalAkhir = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diskon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Kode = table.Column<string>(type: "text", nullable: false),
                    Potongan = table.Column<decimal>(type: "numeric", nullable: false),
                    Tipe = table.Column<string>(type: "text", nullable: false),
                    TanggalMulai = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TanggalAkhir = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mitra",
                columns: table => new
                {
                    IdMitra = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mitra", x => x.IdMitra);
                    table.ForeignKey(
                        name: "FK_Mitra_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pelanggan",
                columns: table => new
                {
                    IdPelanggan = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPengguna = table.Column<string>(type: "text", nullable: false),
                    NomorSIM = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelanggan", x => x.IdPelanggan);
                    table.ForeignKey(
                        name: "FK_Pelanggan_AspNetUsers_IdPengguna",
                        column: x => x.IdPengguna,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motor",
                columns: table => new
                {
                    IdMotor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlatNomor = table.Column<string>(type: "text", nullable: false),
                    IdMitra = table.Column<int>(type: "integer", nullable: false),
                    NomorSTNK = table.Column<string>(type: "text", nullable: false),
                    NomorBPKB = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Tipe = table.Column<string>(type: "text", nullable: false),
                    Tahun = table.Column<int>(type: "integer", nullable: false),
                    Transmisi = table.Column<string>(type: "text", nullable: false),
                    StatusMotor = table.Column<string>(type: "text", nullable: false),
                    HargaHarian = table.Column<decimal>(type: "numeric", nullable: false),
                    DiskonPercentage = table.Column<int>(type: "integer", nullable: true),
                    DiskonAmount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motor", x => x.IdMotor);
                    table.ForeignKey(
                        name: "FK_Motor_Mitra_IdMitra",
                        column: x => x.IdMitra,
                        principalTable: "Mitra",
                        principalColumn: "IdMitra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaksi",
                columns: table => new
                {
                    IdTransaksi = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdMotor = table.Column<int>(type: "integer", nullable: false),
                    IdPelanggan = table.Column<int>(type: "integer", nullable: false),
                    TanggalMulai = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TanggalSelesai = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalHarga = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaksi", x => x.IdTransaksi);
                    table.ForeignKey(
                        name: "FK_Transaksi_Motor_IdMotor",
                        column: x => x.IdMotor,
                        principalTable: "Motor",
                        principalColumn: "IdMotor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaksi_Pelanggan_IdPelanggan",
                        column: x => x.IdPelanggan,
                        principalTable: "Pelanggan",
                        principalColumn: "IdPelanggan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ulasan",
                columns: table => new
                {
                    IdUlasan = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdMotor = table.Column<int>(type: "integer", nullable: false),
                    IdPelanggan = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Komentar = table.Column<string>(type: "text", nullable: false),
                    TanggalUlasan = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ulasan", x => x.IdUlasan);
                    table.ForeignKey(
                        name: "FK_Ulasan_Motor_IdMotor",
                        column: x => x.IdMotor,
                        principalTable: "Motor",
                        principalColumn: "IdMotor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ulasan_Pelanggan_IdPelanggan",
                        column: x => x.IdPelanggan,
                        principalTable: "Pelanggan",
                        principalColumn: "IdPelanggan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pembayaran",
                columns: table => new
                {
                    IdPembayaran = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdTransaksi = table.Column<int>(type: "integer", nullable: false),
                    MetodePembayaran = table.Column<string>(type: "text", nullable: false),
                    StatusPembayaran = table.Column<string>(type: "text", nullable: false),
                    TanggalPembayaran = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pembayaran", x => x.IdPembayaran);
                    table.ForeignKey(
                        name: "FK_Pembayaran_Transaksi_IdTransaksi",
                        column: x => x.IdTransaksi,
                        principalTable: "Transaksi",
                        principalColumn: "IdTransaksi",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mitra_Id",
                table: "Mitra",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Motor_IdMitra",
                table: "Motor",
                column: "IdMitra");

            migrationBuilder.CreateIndex(
                name: "IX_Pelanggan_IdPengguna",
                table: "Pelanggan",
                column: "IdPengguna",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pembayaran_IdTransaksi",
                table: "Pembayaran",
                column: "IdTransaksi");

            migrationBuilder.CreateIndex(
                name: "IX_Transaksi_IdMotor",
                table: "Transaksi",
                column: "IdMotor");

            migrationBuilder.CreateIndex(
                name: "IX_Transaksi_IdPelanggan",
                table: "Transaksi",
                column: "IdPelanggan");

            migrationBuilder.CreateIndex(
                name: "IX_Ulasan_IdMotor",
                table: "Ulasan",
                column: "IdMotor");

            migrationBuilder.CreateIndex(
                name: "IX_Ulasan_IdPelanggan",
                table: "Ulasan",
                column: "IdPelanggan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Diskon");

            migrationBuilder.DropTable(
                name: "Pembayaran");

            migrationBuilder.DropTable(
                name: "Ulasan");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Transaksi");

            migrationBuilder.DropTable(
                name: "Motor");

            migrationBuilder.DropTable(
                name: "Pelanggan");

            migrationBuilder.DropTable(
                name: "Mitra");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
