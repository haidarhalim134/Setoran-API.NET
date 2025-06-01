using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class MitraController : GenericControllerExtension<Mitra>
{
    private readonly Database _db;
    public MitraController(Database db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<Mitra>> CreateMitra([FromBody] PostCreateMitraDTO mitraDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if Pengguna exists
        var pengguna = await _db.Pengguna.FindAsync(mitraDto.IdPengguna);
        if (pengguna is null)
        {
            return NotFound(new { message = "Pengguna tidak ditemukan" });
        }
        // Check if Mitra already exists for this Pengguna
        var existingMitra = await _db.Mitra
            .FirstOrDefaultAsync(m => m.IdPengguna == mitraDto.IdPengguna);
        if (existingMitra != null)
        {
            return BadRequest(new { message = "Pengguna sudah menjadi Mitra" });
        }
        // Create new Mitra
        var mitra = new Mitra
        {
            IdPengguna = mitraDto.IdPengguna,
            Status = mitraDto.Status
        };
        _db.Mitra.Add(mitra);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(CreateMitra), new { id = mitra.IdMitra }, mitra);
    }

    [HttpGet("mitraMotor")]
    public async Task<ActionResult<IEnumerable<MitraMotorDTO>>> GetMitraMotor()
    {
        // Get all Mitra with related Pengguna
        var data = await _db.Mitra
            .Include(m => m.Pengguna)
            .Select(m => new MitraMotorDTO
            {
                Mitra = m,
                JumlahMotor = _db.Motor.Count(motor => motor.IdMitra == m.IdMitra)
            })
            .ToListAsync();

        return Ok(data);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateMitra([FromBody] PostMitraDTO mitraDto)
    {
        var mitraFound = await _db.Mitra.FindAsync(mitraDto.IdMitra);
        if (mitraFound is null)
            return NotFound(new { message = "Mitra tidak ditemukan" });

        _db.UpdateEntry(mitraFound, mitraDto);
        await _db.SaveChangesAsync();

        return Ok();
    }
}

