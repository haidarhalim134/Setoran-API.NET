using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{    
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    // sample
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] dynamic loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
        if (!result.Succeeded)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // You can return a JWT token here if needed
        return Ok(new { message = "Login successful", userId = user.Id });
    }  
}

