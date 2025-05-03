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
public class MitraController : GenericControllerEXtension<Notifikasi>
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
        var mitras = await _db.Mitra
            .Include(m => m.Pengguna)
            .ToListAsync();

        // Map to anonymous objects or a DTO with motor count
        var mitrasWithMotorCount = mitras.Select(async m => new MitraMotorDTO
        {
            Mitra=m,
            JumlahMotor = await _db.Motor.CountAsync(motor => motor.IdMitra == m.IdMitra)
        });

        return Ok(mitrasWithMotorCount);
    }
}

