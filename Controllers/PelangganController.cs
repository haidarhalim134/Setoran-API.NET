using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class PelangganController : ControllerBase
{
    [Authorize]
    [HttpGet("currentPelanggan")]
    public Pelanggan? CurrentPelanggan(Database db)
    {
        return HttpContext.GetCurrentPelanggan(db);
    }
}