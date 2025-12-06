using githubSearch.Api.Models;
using githubSearch.Api.Services;
using Microsoft.AspNetCore.Mvc;


namespace githubSearch.Api.Controllers;

//---------------
// AuthController.cs - JWT Login
//---------------

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwt;

    public AuthController()
    {
        _jwt = new JwtService();
    }


    // Login endpoint to and generate JWT token
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto user)
    {
        // For demo, hardcoded username and password
        if (user.Username == "admin" && user.Password == "1234")
        {
            var token = _jwt.GenerateToken(user.Username);
            return Ok(new { token });
        }
        return Unauthorized(new { message = "Invalid credentials" });
    }
}