using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class PembayaranController : ControllerBase
{
    private readonly Database _context;

    public PembayaranController(Database context)
    {
        _context = context;
    }

    // [Authorize]
    [HttpGet]
    public ActionResult<List<Pembayaran>> GetAll()
    {
        var pembayarans = _context.Pembayaran.ToList();
        if (!pembayarans.Any())
        {
            return NotFound(new { message = "Data pembayaran kosong" });
        }

        return Ok(pembayarans);
    }

    // [Authorize]
    [HttpGet("{id}")]
    public ActionResult<Pembayaran> GetById(int id)
    {
        var pembayaran = _context.Pembayaran.Find(id);
        if (pembayaran == null)
        {
            return NotFound(new { message = "Pembayaran tidak ditemukan" });
        }

        return Ok(pembayaran);
    }

    // [Authorize]
    [HttpGet("transaksi/{id}")]
    public ActionResult<Pembayaran> GetByTransaksiId(int id)
    {
        var pembayaran = _context.Pembayaran
            .FirstOrDefault(p => p.IdTransaksi == id);
        
        if (pembayaran == null)
        {
            return NotFound(new { message = "Pembayaran tidak ditemukan" });
        }

        return Ok(pembayaran);
    }
}