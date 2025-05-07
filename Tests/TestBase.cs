
using Microsoft.EntityFrameworkCore.Storage;

public class TestBase : IDisposable
{
    protected Database _db;
    private readonly IDbContextTransaction _transaction;
    public TestBase()
    {
        Load();
        _db = new Database();
        _transaction = _db.Database.BeginTransaction();
    }

    // biar pastiin .env ke load, bisa jadi bikin ada bug ...
    private void Load(string fileName = ".env")
    {
        var current = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (current != null)
        {
            var envPath = Path.Combine(current.FullName, fileName);
            if (File.Exists(envPath))
            {
                foreach (var line in File.ReadAllLines(envPath))
                {
                    var trimmed = line.Trim();

                    if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
                        continue;

                    var separatorIndex = trimmed.IndexOf('=');
                    if (separatorIndex == -1)
                        continue;

                    var key = trimmed.Substring(0, separatorIndex).Trim();
                    var value = trimmed.Substring(separatorIndex + 1).Trim().Trim('"');

                    Environment.SetEnvironmentVariable(key, value);
                }

                Console.WriteLine("✅ .env variables loaded.");
                return;
            }

            current = current.Parent;
        }

        Console.WriteLine($"⚠️ .env file not found while walking up directories from {Directory.GetCurrentDirectory()}");
    }

    public void Dispose()
    {
        _transaction.Rollback(); 
        _transaction.Dispose();
        _db.Dispose();
        Console.WriteLine("disposed");
    }
}