using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Bogus;
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

    [NotMapped]
    public int? Umur
    {
        get
        {
            if (TanggalLahir == null)
                return null;

            var today = DateTime.Today;
            var age = today.Year - TanggalLahir.Value.Year;
            if (TanggalLahir > today.AddYears(-age)) age--;
            return age;
        }
    }
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

        static DateTime randomDate()
        {
            var random = new Random();
            DateTime start = new DateTime(1990, 1, 1);
            DateTime end = new DateTime(2004, 12, 31);

            int range = (end - start).Days;
            return start.AddDays(random.Next(range + 1)).ToUniversalTime();
        }

        var admin = new Pengguna
        {
            Nama = "admin01",
            UserName = "admin01@mail.com",
            Email = "admin01@mail.com",
            TanggalLahir = randomDate(),
            IsAdmin = true,
            NormalizedUserName = "ADMIN01@MAIL.COM",
            NormalizedEmail = "ADMIN01@MAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            LockoutEnabled = true,
            NomorTelepon = "081234567890",
            NomorKTP = "3201123456789012",
            Alamat = "Jl. Admin No. 1, Kota Admin"
        };
        admin.PasswordHash = hashPassword(admin, "admin1234");
        dbContext.Set<Pengguna>().Add(admin);

        Notifikasi.CreateNotification(admin.Id, "Selamat datang di aplikasi Setoran", 
            "Silahkan selesaikan proses registrasi dengan melengkapi data-data anda di halaman edit profile")
            .ToEditProfile()
            .Send(dbContext);
        
        // Use Bogus to generate fake users
        var faker = new Faker("id_ID");
        var userFaker = new Faker<Pengguna>("id_ID")
            .RuleFor(u => u.Nama, f => f.Name.FullName())
            .RuleFor(u => u.UserName, (f, u) => $"{u.Nama.Replace(" ", ".")}{f.Random.Number(100)}@mail.com".ToLower())
            .RuleFor(u => u.Email, (f, u) => u.UserName)
            .RuleFor(u => u.TanggalLahir, f => randomDate())
            .RuleFor(u => u.NomorTelepon, f => f.Phone.PhoneNumber("08##########"))
            .RuleFor(u => u.NomorKTP, f => f.Random.ReplaceNumbers("32##############"))
            .RuleFor(u => u.Alamat, f => f.Address.FullAddress())
            .RuleFor(u => u.IdGambar, f => $"user-{f.Random.Number(1, 100)}.jpg")
            .RuleFor(u => u.IsAdmin, f => false)
            .RuleFor(u => u.EmailConfirmed, f => true)
            .RuleFor(u => u.SecurityStamp, f => Guid.NewGuid().ToString())
            .RuleFor(u => u.ConcurrencyStamp, f => Guid.NewGuid().ToString())
            .RuleFor(u => u.LockoutEnabled, f => true)
            .FinishWith((f, u) =>
            {
                u.NormalizedUserName = u.UserName.ToUpper();
                u.NormalizedEmail = u.Email.ToUpper();
            });

        var users = new List<Pengguna>();
        for (int i = 0; i < 10; i++)
        {
            var user = userFaker.Generate();
            user.PasswordHash = hashPassword(user, "user1234");
            users.Add(user);
            Notifikasi.CreateNotification(user.Id, "Selamat datang di aplikasi Setoran",
                "Silahkan selesaikan proses registrasi dengan melengkapi data-data anda di halaman edit profile")
                .ToEditProfile()
                .Send(dbContext);
        }

        dbContext.Set<Pengguna>().AddRange(users);
        dbContext.SaveChanges();
    }
}

