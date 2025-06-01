using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dotenv;
using dotenv.net;
using Setoran_API.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;

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
    public DbSet<MotorImage> MotorImage { get; set; }

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

        modelBuilder.Entity<Mitra>()
            .HasOne(e => e.Pengguna)
            .WithOne(e => e.Mitra)
            .HasForeignKey<Mitra>(p => p.IdPengguna);

        modelBuilder.Entity<Notifikasi>()
            .HasOne(e => e.Pengguna)
            .WithMany(e => e.Notifikasis)
            .HasForeignKey(p => p.IdPengguna);

        modelBuilder.Entity<DeviceToken>()
            .HasOne(e => e.Pengguna)
            .WithMany(e => e.DeviceTokens)
            .HasForeignKey(p => p.IdPengguna);

        modelBuilder.Entity<Diskon>()
            .HasOne(e => e.Motor)
            .WithMany(e => e.Diskon)
            .HasForeignKey(p => p.IdMotor);

        modelBuilder.Entity<Voucher>()
            .HasMany(e => e.Pelanggans)
            .WithMany(e => e.UsedVouchers)
            .UsingEntity<VoucherUsed>();

        modelBuilder.Entity<Voucher>()
            .HasIndex(u => u.KodeVoucher)
            .IsUnique();

        modelBuilder.Entity<Notifikasi>()
            .Property(b => b.DataNavigasi)
            .HasConversion(
                v => v == null ? null : JsonConvert.SerializeObject(v),
                v => string.IsNullOrEmpty(v)
                    ? null
                    : JsonConvert.DeserializeObject<Dictionary<string, string>>(v));
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

            // repot nih
            Setoran_API.NET.Models.Pengguna.Seed(context);
            Setoran_API.NET.Models.Mitra.Seed(context);
            Setoran_API.NET.Models.Pelanggan.Seed(context);
            Setoran_API.NET.Models.Motor.Seed(context);
            Setoran_API.NET.Models.Voucher.Seed(context);
            Setoran_API.NET.Models.Transaksi.Seed(context);

            context.SaveChanges();
        });

    }

    /// <summary>
    /// update sebuah entry database, kalau prop null -> ignore
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="oldObject"></param>
    /// <param name="newObject"></param>
    public void UpdateEntry<T>(T oldObject, object newObject, bool ignoreNull = true)
    {
        if (ignoreNull)
        {
            var oldEntry = Entry(oldObject);
            var newType = newObject.GetType();

            foreach (var property in oldEntry.Properties)
            {
                var propertyName = property.Metadata.Name;
                var newProp = newType.GetProperty(propertyName);

                if (newProp != null)
                {
                    dynamic? newValue = newProp.GetValue(newObject);

                    if (newValue != null)
                    {
                        property.CurrentValue = newValue;
                    }
                }
            }
        }
        else
        {
            Entry(oldObject).CurrentValues.SetValues(newObject);
        }
    }
}
