using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorImageController : ControllerBase
    {
        private readonly Database _context;
        private readonly SupabaseService _supabaseService;

        public MotorImageController(Database context, SupabaseService supabaseService)
        {
            _context = context;
            _supabaseService = supabaseService;
        }

        [HttpPost()]
        public async Task<ActionResult<MotorImage>> CreateMotorImage([FromBody] PostMotorImageDTO postMotorImageDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Motor image data is required.");
            }
            var motor = await _context.Motor.FindAsync(postMotorImageDTO.IdMotor);
            if (motor == null)
            {
                return NotFound($"Motor with ID {postMotorImageDTO.IdMotor} not found.");
            }
            var motorImage = new MotorImage
            {
                IdMotor = postMotorImageDTO.IdMotor,
                Front = postMotorImageDTO.Front,
                Left = postMotorImageDTO.Left,
                Right = postMotorImageDTO.Right,
                Rear = postMotorImageDTO.Rear
            };
            _context.MotorImage.Add(motorImage);
            await _context.SaveChangesAsync();

            motor.IdMotorImage = motorImage.Id;
            _context.Motor.Update(motor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateMotorImage), new { id = motorImage.Id }, motorImage);
        }
        
        [HttpPut]
        public async Task<ActionResult<MotorImage>> UpdateMotorImage([FromBody] PutMotorImageDTO putMotorImageDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid motor image data.");
            }

            var motorImage = await _context.MotorImage
                .FirstOrDefaultAsync(mi => mi.IdMotor == putMotorImageDTO.IdMotor);
            if (motorImage == null)
            {
                return NotFound($"Motor image for motor ID {putMotorImageDTO.IdMotor} not found.");
            }

            // kalau sempet delete gambar lama sebelum di update
            if (putMotorImageDTO.Front != null)
                motorImage.Front = await _supabaseService.StoreFile("image", putMotorImageDTO.Front);

            if (putMotorImageDTO.Left != null)
                motorImage.Left = await _supabaseService.StoreFile("image", putMotorImageDTO.Left);

            if (putMotorImageDTO.Right != null)
                motorImage.Right = await _supabaseService.StoreFile("image", putMotorImageDTO.Right);

            if (putMotorImageDTO.Rear != null)
                motorImage.Rear = await _supabaseService.StoreFile("image", putMotorImageDTO.Rear);

            _context.MotorImage.Update(motorImage);
            await _context.SaveChangesAsync();

            return Ok(motorImage);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MotorImage>> GetMotorImageById(int id)
        {
            var motorImage = await _context.MotorImage.FindAsync(id);
            if (motorImage == null)
            {
                return NotFound($"Motor image with ID {id} not found.");
            }
            return Ok(motorImage);
        }
    }
}