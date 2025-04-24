using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dotenv;
using dotenv.net;
using Setoran_API.NET.Models;
using Microsoft.AspNetCore.Identity;

public class Database: IdentityDbContext
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

    public Database()
    {

    }
    public Database(DbContextOptions<Database> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pelanggan>()
            .HasOne(e => e.Pengguna)
            .WithOne(e => e.Pelanggan)
            .HasForeignKey<Pelanggan>(p => p.IdPengguna);

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
            // fallback pake sqlite local
            options.UseSqlite($"Data Source=./database.db");
            // throw new Exception("Database connection variables are missing!");
        } else 
        {
            string connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=True";

            options.UseNpgsql(connectionString);
        }

        options.UseLazyLoadingProxies(false);
        options.UseSeeding((context, _) => {

        });

    }

    public Pengguna? GetCurrentPengguna(HttpContext context)
    {
        return Pengguna.Where(itm => itm.UserName == context!.User!.Identity!.Name).FirstOrDefault();
    }
    public Pelanggan? GetCurrentPelanggan(HttpContext context)
    {
        var pengguna = GetCurrentPengguna(context);
        if (pengguna != null)
        {
            Entry(pengguna).Reference(itm => itm.Pelanggan).Load();
            return pengguna.Pelanggan;
        }

        return null;
    }
}
