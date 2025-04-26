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
public class NotifikasiController : GenericControllerEXtension<Notifikasi>
{    
    private readonly Database _db;
    public NotifikasiController(Database db)
    {
        _db = db;
    }

    [Authorize]
    [HttpPost("read/{id}")]
    public ActionResult ReadNotification([FromRoute] int id)
    {
        var notifikasi = _db.Notifikasi.Find(id);
        if (notifikasi is null)
            return NotFound("Notifikasi tidak ditemukan");

        notifikasi.IsRead = true;
        _db.SaveChanges();

        return Ok();
    }

    [Authorize]
    [HttpPost("registerDevice")]
    public ActionResult RegisterDevice([FromQuery] string token)
    {
        if (token.IsNullOrEmpty())
            return BadRequest("Token tidak valid");

        var pengguna = HttpContext.GetCurrentPengguna(_db);

        var deviceToken = new DeviceToken {
            Pengguna=pengguna,
            Token=token
        };

        _db.DeviceToken.Add(deviceToken);
        _db.SaveChanges();

        return Ok();
    }

    [Authorize]
    [HttpPost("send")]
    public ActionResult SendNotification([FromBody] PostNotifikasDTO notifikasi)
    {
        var target = _db.Pengguna.Find(notifikasi.IdPengguna);
        if (target is null)
            return BadRequest("Pengguna penerima notifikasi tidak ditemukan");

        _db.Add(notifikasi.ToNotif());
        _db.SaveChanges();

        return Ok($"notifikasi terkirim ke {target.Nama}");
    }

    [Authorize]
    [HttpGet("getPerUser")]
    public IEnumerable<GetNotifikasDTO> GetUserNotification()
    {
        var pengguna = HttpContext.GetCurrentPengguna(_db);

        _db.Entry(pengguna).Collection(itm => itm.Notifikasis).Load();

        var result = pengguna.Notifikasis.Select(GetNotifikasDTO.FromNotif);

        return result;
    }
}

