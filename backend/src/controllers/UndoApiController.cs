using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp;
using NutriApp.Controllers.Middleware;

namespace NutriApp.Controllers;

[Route("api/Undo")]
[ApiController]
[Authorize]
public class UndoApiController : ControllerBase
{
    private readonly App _app;
    
    public UndoApiController(App app)
    {
        _app = app;
    }
    
    // PUT api/Undo
    [HttpPut]
    public IActionResult Put()
    {
        var sessionKey = HttpContext.GetSessionKey();
        _app.UserControl.Undo(sessionKey);
        return NoContent();
    }
}