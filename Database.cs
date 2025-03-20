using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
}
