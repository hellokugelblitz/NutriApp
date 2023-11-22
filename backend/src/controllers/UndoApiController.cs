using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp;

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
    public async Task<IActionResult> Put()
    {
        return Ok();
    }
}