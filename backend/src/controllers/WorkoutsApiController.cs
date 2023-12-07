using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;
using NutriApp.Controllers.Middleware;

namespace NutriApp.Controllers;

[Route("api/Workouts")]
[ApiController]
[Authorize]
public class WorkoutsApiController : ControllerBase
{
    private readonly App _app;
    
    public WorkoutsApiController(App app)
    {
        _app = app;
    }
    
    // POST api/Workouts
    [HttpPost]
    public IActionResult CreateWorkout(WorkoutModel workoutModel)
    {
        var user = HttpContext.GetUser();
        _app.HistoryControl.AddWorkout(workoutModel.ToWorkout(), user.UserName);
        return Ok();
    }
    
    // GET api/Workouts/recommended
    [HttpGet("recommended")]
    public ActionResult<IEnumerable<WorkoutModel>> GetRecommendedWorkouts()
    {
        var user = HttpContext.GetUser();
        return _app.WorkoutControl
            .GenerateRecommendedWorkouts(_app.HistoryControl.GetWorkouts(user.UserName))
            .Select(workout => new WorkoutModel(workout)).ToArray();
    }
}