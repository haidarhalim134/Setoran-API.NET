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
    public Pengguna? CurrentPengguna(Database db)
    {
        return HttpContext.GetCurrentPengguna(db);
    }
}