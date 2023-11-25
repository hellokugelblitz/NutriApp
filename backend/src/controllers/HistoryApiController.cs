using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;

namespace NutriApp.Controllers;

[Route("api/History")]
[ApiController]
[Authorize]
public class HistoryApiController : ControllerBase
{
    private readonly App _app;
    
    public HistoryApiController(App app)
    {
        _app = app;
    }
    
    // GET api/History/workouts
    [HttpGet("workouts")]
    public ActionResult<IEnumerable<Entry<Models.Workout>>> GetWorkouts()
    {
        Entry<Models.Workout>[] workouts =
        {
            new() { Value = new() { Name = "Pushups", Minutes = 60, Intensity = 7.5 } },
            new() { Value = new() { Name = "Pullups", Minutes = 30, Intensity = 5.0 } },
            new() { Value = new() { Name = "Squats", Minutes = 45, Intensity = 6.0 } },
            new() { Value = new() { Name = "Running", Minutes = 30, Intensity = 8.0 } },
            new() { Value = new() { Name = "Cycling", Minutes = 60, Intensity = 7.0 } },
            new() { Value = new() { Name = "Swimming", Minutes = 45, Intensity = 9.0 } },
        };
        
        return workouts;
    }
    
    // GET api/History/weights
    [HttpGet("weights")]
    public ActionResult<IEnumerable<Entry<double>>> GetWeights()
    {
        Entry<double>[] weights =
        {
            new() { Value = 100 },
            new() { Value = 99.5 },
            new() { Value = 99.0 },
            new() { Value = 98.5 },
            new() { Value = 98.0 },
            new() { Value = 97.5 },
            new() { Value = 97.0 },
        };

        return weights;
    }
    
    // GET api/History/calories
    [HttpGet("calories")]
    public ActionResult<IEnumerable<Entry<CalorieProgress>>> GetCalories()
    {
        Entry<CalorieProgress>[] calories =
        {
            new() { Value = new() { ActualCalories = 2000, GoalCalories = 2500 } },
            new() { Value = new() { ActualCalories = 2100, GoalCalories = 2500 } },
            new() { Value = new() { ActualCalories = 2200, GoalCalories = 2500 } },
            new() { Value = new() { ActualCalories = 2300, GoalCalories = 2500 } },
            new() { Value = new() { ActualCalories = 2400, GoalCalories = 2500 } },
            new() { Value = new() { ActualCalories = 2500, GoalCalories = 2500 } },
            new() { Value = new() { ActualCalories = 2600, GoalCalories = 2500 } },
        };

        return calories;
    }
    
    // GET api/History/meals
    [HttpGet("meals")]
    public ActionResult<IEnumerable<Entry<Meal>>> GetMeals()
    {
        Entry<Meal>[] meals =
        {
            new() { Value = new() { Name = "Breakfast" } },
            new() { Value = new() { Name = "Lunch" } },
            new() { Value = new() { Name = "Dinner" } },
        };

        return meals;
    }
}