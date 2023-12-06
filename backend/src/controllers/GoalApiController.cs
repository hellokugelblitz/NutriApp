using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;

namespace NutriApp.Controllers;

[Route("api/Goal")]
[ApiController]
[Authorize]
public class GoalApiController : ControllerBase
{
    private readonly App _app;
    
    public GoalApiController(App app)
    {
        _app = app;
    }
    
    // GET api/Goal
    [HttpGet]
    public async Task<ActionResult<Models.GoalModel>> GetGoal()
    {
        // Create a dummy goal
        var goal = new Models.GoalModel
        {
            Type = "lose",
            WeightGoal = 150,
            DailyCalorieGoal = 2000
        };
        return goal;
    }
    
    // PUT api/Goal/{weightGoal}
    [HttpPut("{weightGoal:double}")]
    public async Task<IActionResult> ChangeGoal(double weightGoal)
    {
        return NoContent();
    }
    
    // POST api/Goal/{dailyWeight}
    [HttpPost("{dailyWeight:double}")]
    public async Task<IActionResult> EnterDailyWeight(double dailyWeight)
    {
        return NoContent();
    }
    
    // PUT api/Goal/fitness
    [HttpPut("fitness")]
    public async Task<IActionResult> IncorporateFitness()
    {
        return NoContent();
    }
    
    // DELETE api/Goal/fitness
    [HttpDelete("fitness")]
    public async Task<IActionResult> RemoveFitness()
    {
        return NoContent();
    }
}