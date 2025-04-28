using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorController : ControllerBase
    {
        private readonly Database _context;

        public MotorController(Database context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMotors()
        {
            var motors = await _context.Motor.ToListAsync();
            // return Ok(motors);
            return Ok(new
            {
                succes = true,
                data = motors,
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotorById(int id)
        {
            var motor = await _context.Motor.FindAsync(id);
            return Ok(motor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMotor([FromBody] MotorForm request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var motor = new Motor
            {
                PlatNomor = request.PlatNomor,
                IdMitra = request.IdMitra,
                NomorSTNK = request.NomorSTNK,
                NomorBPKB = request.NomorBPKB,
                Model = request.Model,
                Brand = request.Brand,
                Tipe = request.Tipe,
                Tahun = request.Tahun,
                Transmisi = request.Transmisi,
                StatusMotor = request.StatusMotor,
                HargaHarian = request.HargaHarian
            };

            await _context.Motor.AddAsync(motor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMotorById), new { id = motor.IdMotor }, motor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMotor(int id, [FromBody] PutMotorDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var motor = await _context.Motor.FindAsync(id);
            if (motor == null)
            {
                return NotFound();
            }

            motor.PlatNomor = request.PlatNomor;
            motor.NomorSTNK = request.NomorSTNK;
            motor.NomorBPKB = request.NomorBPKB;
            motor.Model = request.Model;
            motor.Brand = request.Brand;
            motor.Tipe = request.Tipe;
            motor.Tahun = request.Tahun;
            motor.Transmisi = request.Transmisi;
            motor.StatusMotor = request.StatusMotor;
            motor.HargaHarian = request.HargaHarian;

            _context.Motor.Update(motor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
