
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class DiskonController : GenericControllerExtension<Diskon>
{

    private readonly Database _db;

    public DiskonController(Database db)
    {
        _db = db;
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<IEnumerable<Diskon>>> GetAll([FromQuery] bool withMotor=false)
    {   
        if (withMotor)
            return await _db.Diskon
                .Include(m => m.Motor)
                .ToListAsync();

        return await _db.Diskon.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Diskon>> PostDiskon(PostDiskonDTO diskon)
    {
        var newDiskon = diskon.ToDiskon();
        _db.Diskon.Add(newDiskon);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOne), new { id = newDiskon.IdDiskon }, diskon);
    }

    [HttpPut()]
    public async Task<IActionResult> PutDiskon(PutDiskonDTO diskon)
    {
        var diskonFound = _db.Diskon.FirstOrDefault(itm => itm.IdDiskon == diskon.IdDiskon);
        if (diskonFound is null)
            return NotFound(new {message = "Diskon tidak ditemukan"});

        _db.UpdateEntry(diskonFound, diskon);
        _db.SaveChanges();

        return Ok();
    }
}