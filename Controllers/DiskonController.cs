
using Microsoft.AspNetCore.Mvc;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class DiskonController : GenericControllerEXtension<Diskon>
{

    private readonly Database _db;

    public DiskonController(Database db)
    {
        _db = db;
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

        _db.UpdateEntry(diskonFound, diskon.ToDiskon());
        _db.SaveChanges();

        return Ok();
    }
}