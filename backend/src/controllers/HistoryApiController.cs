using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;
using NutriApp.Workout;

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
        var sessionKey = User.FindFirst("SessionKey")!.Value;
        User user = _app.UserControl.GetUser(Guid.Parse(sessionKey));
        return _app.HistoryControl.GetWorkouts(user.UserName).Select(ele =>
        {
            Models.Workout wrk = new Models.Workout();//{ele.Value.Name, ele.Value.Minutes, ele.Value.Intensity};
            wrk.Name = ele.Value.Name;
            wrk.Minutes = ele.Value.Minutes;
            wrk.Intensity = ele.Value.Intensity.Value();
            Models.Entry<Models.Workout> entry = new Models.Entry<Models.Workout>();
            entry.Value = wrk;
            entry.TimeStamp = ele.TimeStamp;
            return entry;
        }).ToArray();
    }
    
    // GET api/History/weights
    [HttpGet("weights")]
    public ActionResult<IEnumerable<Entry<double>>> GetWeights()
    {
        var sessionKey = User.FindFirst("SessionKey")!.Value;
        User user = _app.UserControl.GetUser(Guid.Parse(sessionKey));

        return _app.HistoryControl.Weights(user.UserName).Select(ele =>
        {
            Models.Entry<double> entry = new Models.Entry<double>();
            entry.Value = ele.Value;
            entry.TimeStamp = ele.TimeStamp;
            return entry;
        }).ToArray();
    }
    
    // GET api/History/calories
    [HttpGet("calories")]
    public ActionResult<IEnumerable<Entry<CalorieProgress>>> GetCalories()
    {
        var sessionKey = User.FindFirst("SessionKey")!.Value;
        User user = _app.UserControl.GetUser(Guid.Parse(sessionKey));

        return _app.HistoryControl.GetCalories(user.UserName).Select(ele =>
        {
            Models.Entry<CalorieProgress> entry = new Models.Entry<CalorieProgress>();
            CalorieProgress cal = new CalorieProgress();
            cal.GoalCalories = (int) ele.Value.TargetCalories;
            cal.ActualCalories = (int) ele.Value.ActualCalories;
            entry.Value = cal;
            entry.TimeStamp = ele.TimeStamp;
            return entry;
        }).ToArray();
    }
    
    // GET api/History/meals
    [HttpGet("meals")]
    public ActionResult<IEnumerable<Entry<Meal>>> GetMeals()
    {
        var sessionKey = User.FindFirst("SessionKey")!.Value;
        User user = _app.UserControl.GetUser(Guid.Parse(sessionKey));

        return _app.HistoryControl.GetMeals(user.UserName).Select(ele =>
        {
            Models.Entry<Meal> entry = new Models.Entry<Meal>();
            Food.Meal tempMeal = _app.FoodControl.GetMeal(ele.Value.Name);
            Meal meal = new Meal(tempMeal);
            entry.Value = meal;
            entry.TimeStamp = ele.TimeStamp;
            return entry;
        }).ToArray();
    }
}