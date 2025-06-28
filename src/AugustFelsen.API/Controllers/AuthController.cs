using AugustFelsen.Application.DTOs;
using AugustFelsen.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AugustFelsen.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _authService.RegisterAsync(request);
        
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _authService.LoginAsync(request);
        
        if (!response.Success)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] string refreshToken)
    {
        var response = await _authService.RefreshTokenAsync(refreshToken);
        
        if (!response.Success)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        await _authService.LogoutAsync(userId);
        return Ok(new { message = "Logged out successfully" });
    }
} 