using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class PelangganController : GenericControllerExtension<Pelanggan>
{
    private readonly Database _db;
    public PelangganController(Database db)
    {
        _db = db;
    }

    [Authorize]
    [HttpGet("currentPelanggan")]
    public Pelanggan? CurrentPelanggan()
    {
        return HttpContext.GetCurrentPelanggan(_db);
    }

    // [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdatePelanggan([FromBody] PostPelangganDTO pelangganDto)
    {
        var pelangganFound = await _db.Pelanggan.FindAsync(pelangganDto.IdPelanggan);
        if (pelangganFound is null)
            return NotFound(new { message = "Pelanggan tidak ditemukan"});
        
        _db.UpdateEntry(pelangganFound, pelangganDto);
        await _db.SaveChangesAsync();

        return Ok();
    }
}