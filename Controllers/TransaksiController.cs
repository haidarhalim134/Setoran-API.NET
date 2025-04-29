using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaksiController : ControllerBase
    {
        Database _context;

        public TransaksiController(Database context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransaksi([FromQuery] TransaksiQuery query)
        {
            var transaksis = _context.Transaksi.AsQueryable(); ;


            // filter by IdMotor or IdPelanggan or IdMitra
            if (!string.IsNullOrEmpty(query.IdMotor))
            {
                transaksis = transaksis.Where(t => t.IdMotor.ToString() == query.IdMotor);
            }
            else if (!string.IsNullOrEmpty(query.IdPelanggan))
            {
                transaksis = transaksis.Where(t => t.IdPelanggan.ToString() == query.IdPelanggan);
            }
            else if (!string.IsNullOrEmpty(query.IdMitra))
            {
                // TODO filter by IdMitra
            }

            if (!string.IsNullOrEmpty(query.Status))
            {
                transaksis = transaksis.Where(t => t.Status == query.Status);
            }

            var result = await transaksis.ToListAsync();

            return Ok(new
            {
                succsess = true,
                data = result,
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaksiById(int id)
        {
            var transaksi = await _context.Transaksi.FindAsync(id);
            if (transaksi == null)
            {
                return NotFound(new { success = false, message = "Transaksi tidak ditemukan" });
            }
            return Ok(new
            {
                success = true,
                data = transaksi,
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaksi([FromBody] PostTransaksiDTO transaksi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var motor = await _context.Motor.FindAsync(transaksi.IdMotor);
            if (motor == null)
            {
                return NotFound(new { success = false, message = "Motor tidak ditemukan" });
            }
            if (motor.StatusMotor != "Tersedia")
            {
                return BadRequest(new { success = false, message = "Motor tidak tersedia" });
            }

            var totalHarga = motor.HargaHarian * (transaksi.TanggalSelesai - transaksi.TanggalMulai).Days;

            var newTransaksi = new Transaksi
            {
                IdMotor = transaksi.IdMotor,
                IdPelanggan = transaksi.IdPelanggan,
                TanggalMulai = transaksi.TanggalMulai,
                TanggalSelesai = transaksi.TanggalSelesai,
                TotalHarga = totalHarga,
                Status = "created" // Set default status
            };

            await _context.Transaksi.AddAsync(newTransaksi);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTransaksiById), new { id = newTransaksi.IdTransaksi }, newTransaksi);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaksi(int id, string status)
        {
            var transaksi = await _context.Transaksi.FindAsync(id);
            if (transaksi == null)
            {
                return NotFound(new { success = false, message = "Transaksi tidak ditemukan" });
            }

            transaksi.Status = status;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = transaksi,
            });
        }
    }
}