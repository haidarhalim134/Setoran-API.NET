using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UlasanController : ControllerBase
    {
        Database _context;

        public UlasanController(Database context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ulasan>>> GetUlasan()
        {
            var ulasans = await _context.Ulasan.ToListAsync();
            return Ok(ulasans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ulasan>> GetUlasanById(int id)
        {
            var ulasan = await _context.Ulasan.FindAsync(id);
            if (ulasan == null)
            {
                return NotFound("Ulasan tidak ditemukan");
            }
            return Ok(ulasan);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUlasan([FromBody] PostUlasanDTO ulasan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUlasan = new Ulasan
            {
                IdMotor = ulasan.IdMotor,
                IdPelanggan = ulasan.IdPelanggan,
                Rating = ulasan.Rating,
                Komentar = ulasan.Komentar,
                TanggalUlasan = DateTime.Now
            };

            _context.Ulasan.Add(newUlasan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUlasanById), new { id = newUlasan.IdUlasan }, newUlasan);
        }
    }
}
