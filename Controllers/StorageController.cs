using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Setoran_API.NET.Models;
using Supabase.Storage;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageController : ControllerBase
{    

    private readonly SupabaseService _supabaseService;

    public StorageController(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    [HttpPost("store")]
    public async Task<ActionResult<string>> UploadImage(IFormFile file)
    {

        if (file == null || file.Length == 0)
            return BadRequest(new {message = "No file uploaded."});

        try {
            var fileName = await _supabaseService.StoreFile("image", file);
            return Ok(fileName);
        } catch {
            return StatusCode(500, "Failed to upload image");
        }

    }

    [HttpGet("fetch/{fileName}")]
    public async Task<ActionResult<byte[]>> GetItem([FromRoute] string fileName)
    {
        try {
            var bucket = "image"; 


            var response = await _supabaseService.Client.Storage
                .From(bucket)
                .Download(fileName, (s, p) => {});

            if (response == null)
                return NotFound(new {message = "File not found."});

            return File(response, "application/octet-stream", fileName);
        } catch {
            return NotFound(new {message = "File not found."});
        }

    }
}

