using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dotenv;
using dotenv.net;
using Setoran_API.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

public class Database : IdentityDbContext
{
    public DbSet<Pengguna> Pengguna { get; set; }
    public DbSet<Pelanggan> Pelanggan { get; set; }
    public DbSet<Mitra> Mitra { get; set; }
    public DbSet<Transaksi> Transaksi { get; set; }
    public DbSet<Pembayaran> Pembayaran { get; set; }
    public DbSet<Voucher> Voucher { get; set; }
    public DbSet<VoucherUsed> VoucherUsed { get; set; }
    public DbSet<Ulasan> Ulasan { get; set; }
    public DbSet<Motor> Motor { get; set; }
    public DbSet<Diskon> Diskon { get; set; }
    public DbSet<Notifikasi> Notifikasi { get; set; }
    public DbSet<DeviceToken> DeviceToken { get; set; }

    public Database()
    {

    }
    public Database(DbContextOptions<Database> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // untuk pengguna aja sepertinya mending bikin relasinya disini soalnya primary key Pengguna 'Id' (dari identity), bikin ambigu, dan alasan lain
        modelBuilder.Entity<Pelanggan>()
            .HasOne(e => e.Pengguna)
            .WithOne(e => e.Pelanggan)
            .HasForeignKey<Pelanggan>(p => p.IdPengguna);
            
        modelBuilder.Entity<Notifikasi>()
            .HasOne(e => e.Pengguna)
            .WithMany(e => e.Notifikasis)
            .HasForeignKey(p => p.IdPengguna);

        modelBuilder.Entity<DeviceToken>()
            .HasOne(e => e.Pengguna)
            .WithMany(e => e.DeviceTokens)
            .HasForeignKey(p => p.IdPengguna);

        modelBuilder.Entity<Voucher>()   
            .HasIndex(u => u.KodeVoucher)
            .IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    // => options
    //     .UseSqlite($"Data Source=./database.db")
    //     .UseLazyLoadingProxies(false).
    //     UseSeeding((context, _) => {

    //     });
    {
        //     kalau udah punya postgres
        DotEnv.Load(); // Load environment variables from .env

        string host = Environment.GetEnvironmentVariable("DB_HOST");
        string port = Environment.GetEnvironmentVariable("DB_PORT");
        string dbName = Environment.GetEnvironmentVariable("DB_NAME");
        string user = Environment.GetEnvironmentVariable("DB_USER");
        string password = Environment.GetEnvironmentVariable("DB_PASSWORD");

        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(dbName) ||
            string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
        {
            throw new Exception("Database connection variables are missing!");
        }
        else
        {
            string connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={password};SSL Mode=Prefer;Trust Server Certificate=True";

            options.UseNpgsql(connectionString);
        }

        options.UseLazyLoadingProxies(false);
        options.UseSeeding((context, _) =>
        {
            // berarti udah seed
            if (context.Set<Pengguna>().Any())
                return;

            // agak sumpek kalau seeding semua disini, mungkin refactor seeder setiap entity ke tempat terpisah (e.g. method-method) nanti
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
                NormalizedUserName = "ADMIN01@MAIL.COM",
                NormalizedEmail = "ADMIN01@MAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true
            };

            pengguna.PasswordHash = hashPassword(pengguna, "admin1234");

            context.Set<Pengguna>().Add(pengguna);

            var pelanggan = new Pelanggan
            {
                IdPengguna = pengguna.Id
            };
            
            context.Set<Pelanggan>().Add(pelanggan);

            var notifikasi = Setoran_API.NET.Models.Notifikasi.CreateNotification(pengguna.Id, "Selamat datang di aplikasi Setoran", "Silahkan selesaikan proses registrasi dengan melengkapi data-data anda di halaman edit profile")
                .ToEditProfile();
                // .Send(db);
            
            context.Set<Notifikasi>().Add(notifikasi);

            context.SaveChanges();
        });

    }
}
