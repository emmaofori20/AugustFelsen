using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AugustFelsen.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok("August Felsen API is running!");
    }

    [HttpGet("secure")]
    [Authorize]
    public ActionResult<string> GetSecure()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        var roles = User.FindAll(System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList();

        return Ok(new
        {
            message = "Secure endpoint accessed successfully!",
            userId,
            userEmail,
            roles
        });
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public ActionResult<string> GetAdmin()
    {
        return Ok("Admin endpoint accessed successfully!");
    }
} 