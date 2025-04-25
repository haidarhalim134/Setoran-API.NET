using Supabase;
using Microsoft.Extensions.DependencyInjection;
using dotenv.net;

namespace Setoran_API.NET;

public class SupabaseService
{
    private readonly Client _client;

    public SupabaseService()
    {
        DotEnv.Load();

        var url = Environment.GetEnvironmentVariable("SUPABASE_PROJECT_URL");
        var key = Environment.GetEnvironmentVariable("SUPABASE_PROJECT_KEY");

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = false
        };

        _client = new Client(url, key, options);
        _client.InitializeAsync().Wait();
    }

    public Client Client => _client;

    public async Task<string> StoreFile(string bucket, IFormFile file)
    {
        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            fileBytes = memoryStream.ToArray();
        }

        return await StoreFile(bucket, file.FileName, fileBytes);
    }
    // kalau misalnya mau nerima file dalam bentuk base64 -> byte[] (belum coba sih)
    public async Task<string> StoreFile(string bucket, string fileName, byte[] fileBytes)
    {

        fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";

        var response = await Client.Storage
            .From(bucket)
            .Upload(fileBytes, fileName, new Supabase.Storage.FileOptions { Upsert=true});

        if (response == null)
            throw new Exception("Failed to upload image");

        var publicUrl = Client.Storage
            .From(bucket)
            .GetPublicUrl(fileName);
        
        return fileName;
    }
}