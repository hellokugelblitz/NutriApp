using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        var user = _app.UserControl.GetUser(creds.Username);
        if (user != null)
        {
            return Conflict();
        }
        
        var (sessionKey, newUser) = _app.UserControl.CreateUser(creds.Username, creds.Password, 0, new System.DateTime(), "", "");
        return new AuthResult { Session = sessionKey.ToString() };
    }
    
    // POST api/Auth/login
    [HttpPost("login")]
    public async Task<ActionResult<AuthResult>> Login(Credentials creds)
    {
        try
        {
            var (sessionKey, newUser) = _app.UserControl.Login(creds.Username, creds.Password);
            return new AuthResult { Session = sessionKey.ToString() };
        }
        catch
        {
            return NotFound();
        }
    }
    
    // POST api/Auth/logout
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var sessionKey = User.FindFirst("SessionKey")!.Value;
        _app.UserControl.Logout(Guid.Parse(sessionKey));
        return NoContent();
    }
    
    // POST api/Auth/change-password
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(Credentials creds)
    {
        return NoContent();
    }
}