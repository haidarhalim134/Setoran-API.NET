using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class PenggunaController : ControllerBase
{
    [Authorize]
    [HttpGet("currentPengguna")]
    public async Task<Pengguna?> CurrentPengguna(Database db)
    {
        
        var pengguna = HttpContext.GetCurrentPengguna(db);
        
        // include data pelanggan dan mitra
        await db.Entry(pengguna)
            .Reference(itm => itm.Pelanggan)
            .LoadAsync();
        await db.Entry(pengguna)
            .Reference(itm => itm.Mitra)
            .LoadAsync();

        return pengguna;
    }
}