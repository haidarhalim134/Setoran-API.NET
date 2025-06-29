using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PembayaranController : ControllerBase
    {
        Database _context;

        public PembayaranController(Database context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pembayaran>> GetPembayaranById(int id)
        {
            var pembayaran = await _context.Pembayaran.FindAsync(id);
            if (pembayaran == null)
            {
                return NotFound("Pembayaran tidak ditemukan");
            }
            return Ok(pembayaran);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Pembayaran>>> GetAll()
        {
            var pembayarans = await _context.Pembayaran.ToListAsync();
            if (!pembayarans.Any())
            {
                return NotFound();
            }

            return Ok(pembayarans);
        }

        // [Authorize]
        [HttpGet("transaksi/{id}")]
        public async Task<ActionResult<Pembayaran>> GetByTransaksiId(int id)
        {
            var pembayaran = await _context.Pembayaran
                .FirstOrDefaultAsync(p => p.IdTransaksi == id);

            if (pembayaran == null)
            {
                return NotFound();
            }

            return Ok(pembayaran);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePembayaran([FromBody] PostPembayaranDTO pembayaran)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPembayaran = new Pembayaran
            {
                IdTransaksi = pembayaran.IdTransaksi,
                MetodePembayaran = pembayaran.MetodePembayaran,
                StatusPembayaran = StatusPembayaran.BelumLunas,
                TanggalPembayaran = null
            };

            _context.Pembayaran.Add(newPembayaran);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreatePembayaran), new { id = newPembayaran.IdPembayaran }, newPembayaran);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePembayaran(int id, [FromBody] PutPembayaranDTO pembayaran)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPembayaran = await _context.Pembayaran.FindAsync(id);
            if (existingPembayaran == null)
            {
                return NotFound("Pembayaran tidak ditemukan");
            }

            if (pembayaran.MetodePembayaran != null)
            {
                existingPembayaran.MetodePembayaran = pembayaran.MetodePembayaran;
            }
            if (pembayaran.StatusPembayaran != null)
            {
                existingPembayaran.StatusPembayaran = pembayaran.StatusPembayaran;
            }
            if (pembayaran.TanggalPembayaran != null)
            {
                existingPembayaran.TanggalPembayaran = pembayaran.TanggalPembayaran;
            }

            _context.Pembayaran.Update(existingPembayaran);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("confirmPayment/{idPembayaran}")]
        public async Task<IActionResult> ConfirmPayment([FromRoute] int idPembayaran)
        {
            var pembayaran = await _context.Pembayaran.FindAsync(idPembayaran);
            if (pembayaran == null)
            {
                return NotFound("Pembayaran tidak ditemukan");
            }
            if (pembayaran.StatusPembayaran != StatusPembayaran.MenungguKonfirmasi)
            {
                return BadRequest("Pembayaran sudah terkonfirmasi");
            }

            pembayaran.StatusPembayaran = StatusPembayaran.Lunas;
            _context.Update(pembayaran);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
