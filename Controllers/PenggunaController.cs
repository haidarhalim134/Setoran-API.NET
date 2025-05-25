using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class PenggunaController : GenericControllerExtension<Pengguna, string>
{
    private readonly Database _db;
    public PenggunaController(Database db)
    {
        _db = db;
    }

    [Authorize]
    [HttpGet("currentPengguna")]
    public async Task<Pengguna?> CurrentPengguna()
    {

        var pengguna = HttpContext.GetCurrentPengguna(_db);

        // include data pelanggan dan mitra
        await _db.Entry(pengguna)
            .Reference(itm => itm.Pelanggan)
            .LoadAsync();
        await _db.Entry(pengguna)
            .Reference(itm => itm.Mitra)
            .LoadAsync();

        return pengguna;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<Pengguna?>> GetOneExpanded(Database db, [FromRoute] string id)
    {
        var pengguna = db.Pengguna.Find(id);
        if (pengguna is null)
            return NotFound(new { message = "Item not found" });

        // include data pelanggan dan mitra
        await _db.Entry(pengguna)
            .Reference(itm => itm.Pelanggan)
            .LoadAsync();
        await _db.Entry(pengguna)
            .Reference(itm => itm.Mitra)
            .LoadAsync();

        return pengguna;
    }

    // [Authorize]
    [HttpGet("getAll")]
    public async Task<IEnumerable<Pengguna>> GetAll([FromQuery] bool withMitra = false, [FromQuery] bool withPelanggan = false)
    {
        var penggunas = await _db.Pengguna.ToListAsync();

        // kalau terlalu lambat nanti dibikin dto buat lebih spesifik kebutuhan data nya apa
        return penggunas.Select(async (itm) =>
        {
            if (withPelanggan)
                await _db.Entry(itm)
                    .Reference(itm => itm.Pelanggan)
                    .LoadAsync();

            if (withMitra)
                await _db.Entry(itm)
                    .Reference(itm => itm.Mitra)
                    .LoadAsync();

            return itm;
        }).Select(itm => itm.Result);
    }

    [HttpGet("fromMitra")]
    public async Task<ActionResult<Pengguna>> FromMitra([FromQuery] int idMitra)
    {
        var pengguna = await _db.Pengguna.Where(itm => itm.Mitra.IdMitra == idMitra).FirstOrDefaultAsync();
        if (pengguna is null)
            return NotFound();

        return pengguna;
    }

    // [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdatePengguna([FromBody] PostPenggunaDTO penggunaDto)
    {
        var penggunaFound = await _db.Pengguna.FindAsync(penggunaDto.Id);
        if (penggunaFound is null)
            return NotFound(new { message = "Pengguna tidak ditemukan" });

        _db.UpdateEntry(penggunaFound, penggunaDto);
        await _db.SaveChangesAsync();

        return Ok();
    }
    
}