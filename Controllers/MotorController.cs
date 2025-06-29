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
        public async Task<ActionResult<List<Motor>>> GetMotors([FromQuery] MotorQuery query)
        {
            IQueryable<Motor> motors = _context.Motor;

            if (query.WithImage)
            {
                motors = motors.Include(m => m.MotorImage);
            }
            if (query.WithDiskon || query.Sorting == MotorSorting.BestDiscount)
            {
                motors = motors.Include(m => m.Diskon);
            }
            if (query.WithUlasan || query.Sorting == MotorSorting.BestRating)
            {
                motors = motors.Include(m => m.Ulasan);
            }
            if (query.WithPengguna)
            {
                motors = motors
                    .Include(m => m.Mitra)
                    .ThenInclude(m => m.Pengguna);
            }


            if (!string.IsNullOrEmpty(query.IdMitra))
            {
                motors = motors.Where(m => m.IdMitra == int.Parse(query.IdMitra));
            }
            if (query.Status != null)
            {
                motors = motors.Where(m => m.StatusMotor == query.Status);
            }
            if (!string.IsNullOrEmpty(query.Model))
            {
                motors = motors.Where(m => m.Model == query.Model);
            }
            if (query.Transmisi != null)
            {
                motors = motors.Where(m => m.Transmisi == query.Transmisi);
            }

            switch (query.Sorting)
            {
                case MotorSorting.MostPopular:
                    motors = motors
                        .GroupJoin(
                            _context.Transaksi,
                            motor => motor.IdMotor,
                            transaksi => transaksi.IdMotor,
                            (motor, transaksis) => new { Motor = motor, TransaksiCount = transaksis.Count() }
                        )
                        .OrderByDescending(x => x.TransaksiCount)
                        .Select(x => x.Motor);
                    break;

                case MotorSorting.BestRating:
                    motors = motors
                        .OrderByDescending(m => m.Ulasan.Any()
                            ? m.Ulasan.Average(u => u.Rating)
                            : 2.5); // kalau gak ada ulasan anggap average
                    break;

                case MotorSorting.None:
                default:
                    // no sorting applied
                    break;

            }

            // Pagination
            motors = motors
                .Skip((query.Page - 1) * query.AmountPerPage)
                .Take(query.AmountPerPage);

            var result = await motors.ToListAsync();

            // agak repot kalau mau di atas soalnya manggil method custom
            if (query.Sorting == MotorSorting.BestDiscount)
            {
                result = result
                    .OrderByDescending(m => m.GetBestDiscount())
                    .ToList();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Motor>> GetMotorById(int id)
        {
            var motor = await _context.Motor.FindAsync(id);
            if (motor == null)
            {
                return NotFound("Motor tidak ditemukan");
            }
            return Ok(motor);

        }

        [HttpGet("{id}/ulasans")]
        public async Task<ActionResult<List<Ulasan>>> GetUlasanByMotorId(int id)
        {
            var motor = await _context.Motor.FindAsync(id);

            if (motor == null)
            {
                return NotFound("Motor tidak ditemukan");
            }

            var ulasans = await _context.Ulasan.Where(u => u.IdMotor == id).ToListAsync();

            return Ok(ulasans);
        }

        [HttpGet("{id}/ulasan/pelanggan/{idPelanggan}")]
        public async Task<ActionResult<List<Ulasan>>> GetUlasanByMotorIdPenggunaId([FromRoute] int id, [FromRoute] int idPelanggan)
        {
            var motor = await _context.Motor.FindAsync(id);

            if (motor == null)
            {
                return NotFound("Motor tidak ditemukan");
            }

            var ulasans = await _context.Ulasan.Where(u => u.IdMotor == id && u.IdPelanggan == idPelanggan).ToListAsync();

            return Ok(ulasans);
        }

        [HttpGet("{id}/diskons")]
        public async Task<ActionResult<List<Diskon>>> GetDiskonByMotorId(int id)
        {
            var motor = await _context.Motor.FindAsync(id);

            if (motor == null)
            {
                return NotFound("Motor tidak ditemukan");
            }

            var diskons = await _context.Diskon.Where(u => u.IdMotor == id).ToListAsync();

            return Ok(diskons);
        }

        [HttpPost]
        public async Task<ActionResult<Motor>> CreateMotor([FromBody] MotorForm request)
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
                return NotFound("Motor tidak ditemukan");
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

        [HttpGet("acceptMotor/{idMotor}")]
        public async Task<IActionResult> AcceptMotor([FromRoute] int idMotor)
        {
            var motor = await _context.Motor.FindAsync(idMotor);

            if (motor == null)
            {
                return NotFound("Motor tidak ditemukan");
            }

            if (motor.StatusMotor != StatusMotor.Diajukan)
            {
                return BadRequest("Motor sudah diterima");
            }

            motor.StatusMotor = StatusMotor.Tersedia;
            _context.Motor.Update(motor);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotor(int id)
        {
            var motor = await _context.Motor.FindAsync(id);
            if (motor == null)
            {
                return NotFound("Motor tidak ditemukan");
            }

            _context.Motor.Remove(motor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
