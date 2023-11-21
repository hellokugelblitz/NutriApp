using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;

namespace NutriApp.Controllers;

[Route("api/Auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly App _app;
    
    public AuthController(App app)
    {
        _app = app;
    }
    
    // POST api/Auth/signup
    [HttpPost("signup")]
    public async Task<ActionResult<AuthResult>> SignUp(Credentials creds)
    {
        return new AuthResult { Session = "nutrisession" };
    }
    
    // POST api/Auth/login
    [HttpPost("login")]
    public async Task<ActionResult<AuthResult>> Login(Credentials creds)
    {
        return new AuthResult { Session = "nutrisession" };
    }
    
    // POST api/Auth/logout
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return NoContent();
    }
    
    // POST api/Auth/change-password
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(Credentials creds)
    {
        return NoContent();
    }
}