using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;

namespace NutriApp.Controllers;

[Route("api/Workouts")]
[ApiController]
public class WorkoutsApiController : ControllerBase
{
    private readonly App _app;
    
    public WorkoutsApiController(App app)
    {
        _app = app;
    }
    
    // POST api/Workouts
    [HttpPost]
    public async Task<ActionResult<Models.Workout>> CreateWorkout(Models.Workout workout)
    {
        return workout;
    }
    
    // GET api/Workouts/recommended
    [HttpGet("recommended")]
    public async Task<ActionResult<IEnumerable<Models.Workout>>> GetRecommendedWorkouts()
    {
        Models.Workout[] workouts =
        {
            // Some dummy workout structs
            new() { Name = "Running", Minutes = 30, Intensity = 7.5 },
            new() { Name = "Weightlifting", Minutes = 45, Intensity = 10 },
            new() { Name = "Yoga", Minutes = 60, Intensity = 5 },
        };
        return workouts;
    }
}