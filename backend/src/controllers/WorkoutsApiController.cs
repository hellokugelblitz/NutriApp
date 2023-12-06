using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;

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
        var sessionKey = User.FindFirst("SessionKey")!.Value;
        User user = _app.UserControl.GetUser(Guid.Parse(sessionKey));
        _app.HistoryControl.AddWorkout(workoutModel.ToWorkout(), user.UserName);
        return Ok();
    }
    
    // GET api/Workouts/recommended
    [HttpGet("recommended")]
    public async Task<ActionResult<IEnumerable<WorkoutModel>>> GetRecommendedWorkouts()
    {
        var sessionKey = User.FindFirst("SessionKey")!.Value;
        User user = _app.UserControl.GetUser(Guid.Parse(sessionKey));
        WorkoutModel[] workouts =
        {
            // Some dummy workout structs
            new() { Name = "Running", Minutes = 30, Intensity = 7.5 },
            new() { Name = "Weightlifting", Minutes = 45, Intensity = 10 },
            new() { Name = "Yoga", Minutes = 60, Intensity = 5 },
        };
        return _app.WorkoutControl.GenerateRecommendedWorkouts(_app.HistoryControl.GetWorkouts(user.UserName)).Select(ele =>
        {
            return new WorkoutModel(ele);
        }).ToArray();
    }
}