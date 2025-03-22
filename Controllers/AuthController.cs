using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{    
    private readonly SignInManager<Pengguna> _signInManager;
    private readonly UserManager<Pengguna> _userManager;

    public AuthController(SignInManager<Pengguna> signInManager, UserManager<Pengguna> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(Database db, [FromBody] RegisterForm request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new Pengguna
        {
            UserName = request.email,
            Email = request.email,
        };

        var result = await _userManager.CreateAsync(user, request.password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        // Create Pelanggan entry
        var pelanggan = new Pelanggan
        {
            IdPengguna = user.Id
        };
        db.Pelanggan.Add(pelanggan);

        await db.SaveChangesAsync();

        return Ok(new { Message = "Registrasi Berhasil!", User = user });
    }
}

