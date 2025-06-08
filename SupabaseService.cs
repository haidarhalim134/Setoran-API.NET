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
    public async Task<string?> StoreFile(string bucket, string? base64)
    {
        if (string.IsNullOrWhiteSpace(base64)) return null;

        try
        {
            var base64Parts = base64.Split(",", 2);
            var header = base64Parts.Length > 1 ? base64Parts[0] : "";
            var bytes = Convert.FromBase64String(base64Parts.Length > 1 ? base64Parts[1] : base64Parts[0]);

            string extension = ".jpg"; 
            var match = System.Text.RegularExpressions.Regex.Match(header, @"data:image/(?<type>\w+);base64");
            if (match.Success)
            {
                extension = "." + match.Groups["type"].Value;
            }
            // label gak penting soalnya cuma di ambil extension
            return await StoreFile(bucket, "label" + extension, bytes);
        }
        catch
        {
            throw new Exception($"Failed to process base64 image for.");
        }
    }
    // kalau misalnya mau nerima file dalam bentuk base64 -> byte[] (belum coba sih)
    public async Task<string> StoreFile(string bucket, string fileName, byte[] fileBytes)
    {

        fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";

        var response = await Client.Storage
            .From(bucket)
            .Upload(fileBytes, fileName, new Supabase.Storage.FileOptions { Upsert = true });

        if (response == null)
            throw new Exception("Failed to upload image");

        var publicUrl = Client.Storage
            .From(bucket)
            .GetPublicUrl(fileName);

        return fileName;
    }

    public async Task DeleteFile(string bucket, string filename)
    {
        var response = await Client.Storage.From(bucket).Remove(filename);

        if (response == null)
            throw new Exception("Failed to upload image");
    }
}