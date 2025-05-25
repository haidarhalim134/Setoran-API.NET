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
            return NotFound(new { message = "Mitra tidak ditemukan"});
        
        _db.UpdateEntry(mitraFound, mitraDto);
        await _db.SaveChangesAsync();

        return Ok();
    }
}

