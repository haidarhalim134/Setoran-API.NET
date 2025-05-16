using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Setoran_API.NET.Models;

public class Pengguna : IdentityUser
{
    public Pelanggan? Pelanggan { get; set; }
    public Mitra? Mitra { get; set; }

    [DefaultValue(false)]
    public bool IsAdmin { get; set; } = false;
    public List<Notifikasi> Notifikasis { get; set; }
    public List<DeviceToken> DeviceTokens { get; set; }
    public string Nama { get; set; }
    public DateTime? TanggalLahir { get; set; }
    public string NomorTelepon { get; set; }
    public int? Umur { get; set; }
    public string NomorKTP { get; set; }
    public string Alamat { get; set; }
    public string? IdGambar { get; set; }

    public static void Seed(DbContext dbContext)
    {
        static string hashPassword(Pengguna user, string password)
        {
            var passwordHasher = new PasswordHasher<Pengguna>();
            return passwordHasher.HashPassword(user, password);
        }

        var pengguna = new Pengguna
        {
            Nama = "admin01",
            UserName = "admin01@mail.com",
            Email = "admin01@mail.com",
            IsAdmin=true,
            NormalizedUserName = "ADMIN01@MAIL.COM",
            NormalizedEmail = "ADMIN01@MAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            LockoutEnabled = true
        };

        pengguna.PasswordHash = hashPassword(pengguna, "admin1234");

        dbContext.Set<Pengguna>().Add(pengguna);
        dbContext.SaveChanges();

        // seed ke pelanggan
        var pelanggan = new Pelanggan
        {
            IdPengguna = pengguna.Id
        };
        dbContext.Set<Pelanggan>().Add(pelanggan);

        // seed ke mitra
        var Mitra = new Mitra
        {
            IdPengguna = pengguna.Id
        };
        dbContext.Set<Mitra>().Add(Mitra);

        var notifikasi = Setoran_API.NET.Models.Notifikasi.CreateNotification(pengguna.Id, "Selamat datang di aplikasi Setoran", "Silahkan selesaikan proses registrasi dengan melengkapi data-data anda di halaman edit profile")
                .ToEditProfile();
        // .Send(db);

        dbContext.Set<Notifikasi>().Add(notifikasi);
        dbContext.SaveChanges();
    }
}

