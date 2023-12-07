using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;
using NutriApp.Controllers.Middleware;
using NutriApp.Food;
using NutriApp.Undo;

namespace NutriApp.Controllers;

[Route("api/Meals")]
[ApiController]
public class MealsApiController : ControllerBase
{
    private readonly App _app;
    
    public MealsApiController(App app)
    {
        _app = app;
    }
    
    // GET api/Meals
    [HttpGet]
    public ActionResult<IEnumerable<MealModel>> GetMeals()
        => _app.FoodControl.Meals.Select(MealModel.FromMeal).ToList();
    
    // GET api/Meals/{name}
    [HttpGet("{name}")]
    public ActionResult<MealModel> GetMeal(string name)
    {
        var meal = _app.FoodControl.GetMeal(name);
        return meal != null ? MealModel.FromMeal(meal) : new MealModel();
    }
    
    // GET api/Meals/search/{name}
    [HttpGet("search/{name}")]
    public ActionResult<IEnumerable<MealModel>> SearchMeals(string name)
     => _app.FoodControl.SearchMeals(name)
         .Select(MealModel.FromMeal).ToList();
    
    // POST api/Meals
    [HttpPost]
    [Authorize]
    public ActionResult<MealModel> CreateMeal(CreateMealInfo info)
    {
        if (_app.FoodControl.GetMeal(info.Name) != null)
            return BadRequest("Meal already exists; use PUT to edit");

        var meal = new Meal(info.Name);
        var badRecipes = new List<string>();
        
        foreach (var (recipeName, amount) in info.Recipes)
        {
            var recipe = _app.FoodControl.GetRecipe(recipeName);
            if (recipe == null)
                badRecipes.Add(recipeName);
            else
                meal.AddChild(recipe, amount);
        }
        
        if (badRecipes.Count > 0)
            return BadRequest(new { message = "Some recipes don't exist", badRecipes });
        
        _app.FoodControl.AddMeal(meal);

        var sessionKey = HttpContext.GetSessionKey();
        _app.UserControl.AddUndoCommand(sessionKey, new UndoCreateMeal(_app, meal));
        
        return NoContent();
    }
    
    // PUT api/Meals/{name}
    [HttpPut("{name}")]
    [Authorize]
    public IActionResult EditMeal(string name, CreateMealInfo info)
    {

        Meal meal = _app.FoodControl.GetMeal(name);
        meal.Children.Clear();
        foreach (var recipe in info.Recipes.Keys)
        {
            meal.AddChild(_app.FoodControl.GetRecipe(recipe), info.Recipes[recipe]);
        }
        
        return NoContent();
    }
    
    // POST api/Meals/consume/{name}
    [HttpPost("consume/{name}")]
    [Authorize]
    public IActionResult ConsumeMeal(string name)
    {
        if (_app.FoodControl.GetMeal(name) == null)
            return BadRequest("Meal does not exist");
        
        var user = HttpContext.GetUser();

        if (!_app.FoodControl.ConsumeMeal(name, user.UserName)) 
            return BadRequest("Not enough ingredients");
        
        var sessionKey = HttpContext.GetSessionKey();
        _app.UserControl.AddUndoCommand(sessionKey, new UndoConsumeMeal(_app, user, name));
        
        return NoContent();
    }
}