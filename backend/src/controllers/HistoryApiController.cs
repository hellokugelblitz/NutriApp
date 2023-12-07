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
    private string WORKOUT_TYPE = "WORKOUT";
    private string WEIGHT_TYPE = "WEIGHT";
    private string CALORIE_TYPE = "CALORIE";
    private string MEAL_TYPE = "MEAL";
    
    public HistoryApiController(App app)
    {
        _app = app;
    }
    
    // GET api/History/workouts
    [HttpGet("workouts")]
    public ActionResult<IEnumerable<EntryModel<Models.WorkoutModel>>> GetWorkouts()
    {
        var sessionKey = User.FindFirst("SessionKey")!.Value;
        User user = _app.UserControl.GetUser(Guid.Parse(sessionKey));
        return _app.HistoryControl.GetWorkouts(user.UserName).Select(ele =>
        {
            WorkoutModel wrk = new WorkoutModel();//{ele.Value.Name, ele.Value.Minutes, ele.Value.Intensity};
            wrk.Name = ele.Value.Name;
            wrk.Minutes = ele.Value.Minutes;
            wrk.Intensity = ele.Value.Intensity.Value();
            EntryModel<WorkoutModel> entry = new Models.EntryModel<WorkoutModel>();
            entry.Value = wrk;
            entry.TimeStamp = ele.TimeStamp;
            entry.Type = WORKOUT_TYPE;
            return entry;
        }).ToArray();
    }
    
    // GET api/History/weights
    [HttpGet("weights")]
    public ActionResult<IEnumerable<EntryModel<double>>> GetWeights()
    {
        var sessionKey = User.FindFirst("SessionKey")!.Value;
        User user = _app.UserControl.GetUser(Guid.Parse(sessionKey));

        return _app.HistoryControl.Weights(user.UserName).Select(ele =>
        {
            EntryModel<double> entry = new EntryModel<double>();
            entry.Value = ele.Value;
            entry.TimeStamp = ele.TimeStamp;
            entry.Type = WEIGHT_TYPE;
            return entry;
        }).ToArray();
    }
    
    // GET api/History/calories
    [HttpGet("calories")]
    public ActionResult<IEnumerable<EntryModel<CalorieProgressModel>>> GetCalories()
    {
        var sessionKey = User.FindFirst("SessionKey")!.Value;
        User user = _app.UserControl.GetUser(Guid.Parse(sessionKey));

        return _app.HistoryControl.GetCalories(user.UserName).Select(ele =>
        {
            Models.EntryModel<CalorieProgressModel> entry = new Models.EntryModel<CalorieProgressModel>();
            CalorieProgressModel cal = new CalorieProgressModel();
            cal.GoalCalories = (int) ele.Value.TargetCalories;
            cal.ActualCalories = (int) ele.Value.ActualCalories;
            entry.Value = cal;
            entry.TimeStamp = ele.TimeStamp;
            entry.Type = CALORIE_TYPE;
            return entry;
        }).ToArray();
    }
    
    // GET api/History/meals
    [HttpGet("meals")]
    public ActionResult<IEnumerable<EntryModel<MealModel>>> GetMeals()
    {
        var sessionKey = User.FindFirst("SessionKey")!.Value;
        User user = _app.UserControl.GetUser(Guid.Parse(sessionKey));

        return _app.HistoryControl.GetMeals(user.UserName).Select(ele =>
        {
            Models.EntryModel<MealModel> entry = new Models.EntryModel<MealModel>();
            Food.Meal tempMeal = _app.FoodControl.GetMeal(ele.Value.Name);
            MealModel meal = MealModel.FromMeal(tempMeal);
            entry.Value = meal;
            entry.TimeStamp = ele.TimeStamp;
            entry.Type = MEAL_TYPE;
            return entry;
        }).ToArray();
    }
}