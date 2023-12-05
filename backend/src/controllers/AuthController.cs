using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;
using NutriApp.Controllers.Middleware;

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
    
    // GET api/Auth
    [HttpGet]
    [Authorize]
    public ActionResult<User> GetUser()
    {
        var sessionKey = Request.Headers[NutriAppAuthHandler.SessionHeaderName][0]!;
        return _app.UserControl.GetUser(Guid.Parse(sessionKey));
    }
    
    // POST api/Auth/signup
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpInfo info)
    {
        if (_app.UserControl.UserExists(info.Username))
        {
            return Conflict();
        }
        
        var (sessionKey, newUser) = _app.UserControl.CreateUser(
            info.Username,
            info.Password,
            info.Height,
            info.Birthday,
            info.Name,
            ""
            );
        
        _app.HistoryControl.SetWeight(info.CurrentWeight, info.Username);
        _app.GoalControl.SetGoalBasedOnWeightDifference(info.WeightGoal, info.Username);
        _app.UserControl.Logout(sessionKey);
        return Ok();
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
        catch (InvalidUsernameException e)
        {
            return NotFound("Username not found");
        }
        catch (InvalidPasswordException e)
        {
            return NotFound("Username and password combination not found");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"{e.Message}\n{e.StackTrace}");
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