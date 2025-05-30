using Microsoft.AspNetCore.Mvc;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorImageController : ControllerBase
    {
        private readonly Database _context;

        public MotorImageController(Database context)
        {
            _context = context;
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