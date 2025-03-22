using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dotenv;
using dotenv.net;
using Setoran_API.NET.Models;
using Microsoft.AspNetCore.Identity;

public class Database: IdentityDbContext<Pengguna, IdentityRole<int>, int>
{
    public DbSet<Pengguna> Pengguna { get; set; }
    public DbSet<Pelanggan> Pelanggan { get; set; }

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
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options
        .UseSqlite($"Data Source=./database.db")
        .UseLazyLoadingProxies(false);
    // {
    //     kalau udah punya postgres
    //     DotEnv.Load(); // Load environment variables from .env
    //     var envVars = DotEnv.Read();

    //     string host = envVars["DB_HOST"];
    //     string port = envVars["DB_PORT"];
    //     string dbName = envVars["DB_NAME"];
    //     string user = envVars["DB_USER"];
    //     string password = envVars["DB_PASSWORD"];

    //     if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(dbName) || 
    //         string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
    //     {
    //         throw new Exception("Database connection variables are missing!");
    //     }

    //     string connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={password}";

    //     options.UseNpgsql(connectionString);
    //     options.UseLazyLoadingProxies(false);

    // }

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
