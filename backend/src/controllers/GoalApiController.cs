using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;
using NutriApp.Controllers.Middleware;
using NutriApp.Goal;

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
    public ActionResult<GoalModel> GetGoal()
    {
        var user = HttpContext.GetUser();
        var goal = _app.GoalControl.GetGoal(user.UserName);
        var currentWeight = _app.HistoryControl.CurrentWeight(user.UserName);
        
        var targetMinusCurrent = goal.WeightGoal - currentWeight;
        
        return new GoalModel
        {
            Type = targetMinusCurrent switch
            {
                > 5 => "gain",
                < -5 => "lose",
                _ => "maintain"
            },
            WeightGoal = goal.WeightGoal,
            DailyCalorieGoal = goal.DailyCalorieGoal
        };
    }
    
    // PUT api/Goal/{weightGoal}
    [HttpPut("{weightGoal:double}")]
    public IActionResult ChangeGoal(double weightGoal)
    {
        var user = HttpContext.GetUser();
        _app.GoalControl.SetGoalBasedOnWeightDifference(weightGoal, user.UserName);
        return NoContent();
    }
    
    // POST api/Goal/{dailyWeight}
    [HttpPost("{dailyWeight:double}")]
    public IActionResult EnterDailyWeight(double dailyWeight)
    {
        var user = HttpContext.GetUser();
        _app.HistoryControl.SetWeight(dailyWeight, user.UserName);
        return NoContent();
    }
    
    // PUT api/Goal/fitness
    [HttpPut("fitness")]
    public IActionResult IncorporateFitness()
    {
        _app.GoalControl.IncorporateFitness(HttpContext.GetUser().UserName);
        return NoContent();
    }
    
    // DELETE api/Goal/fitness
    [HttpDelete("fitness")]
    public IActionResult RemoveFitness()
    {
        var currentGoal = _app.GoalControl.GetGoal(HttpContext.GetUser().UserName);
        if (currentGoal is not FitnessGoal)
            return BadRequest("The current goal is not a fitness goal.");
        
        _app.GoalControl.SetGoalBasedOnWeightDifference(currentGoal.WeightGoal, HttpContext.GetUser().UserName);
        return NoContent();
    }
}