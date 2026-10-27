using EmergencyPatientMonitoring.API.DTOs;
using EmergencyPatientMonitoring.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyPatientMonitoring.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth) => _auth = auth;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var (result, errors) = await _auth.RegisterAsync(dto);
        if (result is null) return BadRequest(new { errors });
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _auth.LoginAsync(dto);
        if (result is null) return Unauthorized(new { message = "Invalid email or password." });
        return Ok(result);
    }
}