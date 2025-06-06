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
                AvailableMotorCount = await _db.Motor.Where(m => m.StatusMotor == "Tersedia").CountAsync(),
                FiledMotorCount = await _db.Motor.Where(m => m.StatusMotor == "Diajukan").CountAsync(),
                TransaksiOngoing = await _db.Transaksi.Where(t => t.Status == "berlangsung").CountAsync(),
                TotalIncome = await _db.Transaksi.SumAsync(t => t.TotalHarga),
                ChartTransaksi = await _db.Transaksi.Where(t => t.Status == "selesai").ToListAsync(),
            };
            return Ok(result);
        }
    }
}
