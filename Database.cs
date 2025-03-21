using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dotenv;
using dotenv.net;

class Database: IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }

    public Database()
    {

    }
    public Database(DbContextOptions<Database> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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
}
