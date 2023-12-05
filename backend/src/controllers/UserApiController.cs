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

    // GET api/User
    // [HttpGet]
    // public ActionResult<User> GetUser(UserProfile user)
    // {
    //     var username = user.Name;
    //     return _app.UserControl.GetUser(username);
    // }
    

    // [HttpGet]
    // public ActionResult<User> GetUser(string username)
    // {
    //     return _app.UserControl.GetUser(username);
    // }


    [HttpGet("{username}")]
    public ActionResult<User> GetUser(string username)
    {
        Console.WriteLine($"Debugging: Retrieving user for username '{username}'");
        return _app.UserControl.GetUser(username);
    }
}