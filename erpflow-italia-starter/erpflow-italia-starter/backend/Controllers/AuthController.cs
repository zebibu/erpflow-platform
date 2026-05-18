using Microsoft.AspNetCore.Mvc;

namespace ERPFlowItalia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok(new
        {
            message = "JWT login will be implemented in the next step.",
            demoToken = "demo-token",
            role = "Admin"
        });
    }

    [HttpPost("register")]
    public IActionResult Register()
    {
        return Ok(new
        {
            message = "User registration will be implemented in the next step."
        });
    }
}
