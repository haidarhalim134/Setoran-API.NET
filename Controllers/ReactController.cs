using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {
        Database _db;

        public ReactController(Database db)
        {
            _db = db;
        }

        [HttpGet("dashboardData")]
        public async Task<ActionResult<DashboardDataDTO>> GetDashboardData()
        {
            var result = new DashboardDataDTO
            {
                AvailableMotorCount = await _db.Motor.Where(m => m.StatusMotor == StatusMotor.Tersedia).CountAsync(),
                FiledMotorCount = await _db.Motor.Where(m => m.StatusMotor == StatusMotor.Diajukan).CountAsync(),
                TransaksiOngoing = await _db.Transaksi.Where(t => t.Status == StatusTransaksi.Berlangsung).CountAsync(),
                TotalIncome = await _db.Transaksi.SumAsync(t => t.TotalHarga),
                ChartTransaksi = await _db.Transaksi.Where(t => t.Status == StatusTransaksi.Selesai).ToListAsync(),
            };
            return Ok(result);
        }

        [HttpGet("motorTableData")]
        public async Task<ActionResult<List<MotorTableDTO>>> GetMotorTableData()
        {
            static MotorTableDTO toDto(Motor motor)
            {
                MotorTableDTO data = motor.Adapt<MotorTableDTO>();
                data.OwnerName = motor.Mitra.Pengguna.Nama;
                data.OwnerId = motor.Mitra.Pengguna.Id;

                return data;
            }
            return _db.Motor
                .Include(m => m.Mitra)
                .ThenInclude(mitra => mitra.Pengguna)
                .Select(toDto)
                .ToList();
        }
    }
}
