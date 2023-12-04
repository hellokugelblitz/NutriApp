using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;

namespace NutriApp.Controllers;

[Route("api/InitialSignup")]
[ApiController]
[Authorize]
public class InitialSignupApiController : ControllerBase
{
    private readonly App _app;
    
    public InitialSignupApiController(App app)
    {
        _app = app;
    }
    
    // POST api/InitialSignup
    [HttpPost]
    public async Task<IActionResult> PostInitialSignup(InitialUserInfo info)
    {
        return Ok();
    }
}