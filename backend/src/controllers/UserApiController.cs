using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;
using NutriApp.Controllers.Middleware;

namespace NutriApp.Controllers;

[Route("api/User")]
[ApiController]
public class UserApiController : ControllerBase
{
    private readonly App _app;
    
    public UserApiController(App app)
    {
        _app = app;
    }

    [HttpGet("{username}")]
    public ActionResult<User> GetUser(string username)
    {
        return _app.UserControl.GetUser(username);
    }

    // POST api/User/update
    [HttpPost("update")]
    [Authorize]
    public IActionResult UpdateUser(UpdateInfo info)
    {
        var user = HttpContext.GetUser();
        if(info.Password != "") _app.UserControl.ChangePassword(user.UserName, info.Password);
        user.UpdateUser(info);

        return Ok();
    }
}