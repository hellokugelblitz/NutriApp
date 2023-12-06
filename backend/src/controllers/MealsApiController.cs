using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;
using NutriApp.Controllers.Middleware;
using NutriApp.Food;

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
    {
        // TODO: Implement search on backend

        // Some dummy MealModels
        MealModel[] meals =
        {
            new()
            {
                Name = "Breakfast",
                Recipes = new List<MealRecipeEntry>
                {
                    new()
                    {
                        Recipe = new RecipeModel
                        {
                            Name = "Apple Pie",
                            Ingredients = new List<RecipeIngredientEntry>
                            {
                                new()
                                {
                                    Ingredient = new IngredientModel
                                    {
                                        Name = "Apple",
                                        Calories = 95,
                                        Fat = 0.3,
                                        Protein = 0.5,
                                        Fiber = 4.4,
                                        Carbs = 25.1
                                    },
                                    Amount = 1
                                },
                                new()
                                {
                                    Ingredient = new IngredientModel
                                    {
                                        Name = "Pie Crust",
                                        Calories = 100,
                                        Fat = 0.3,
                                        Protein = 0.5,
                                        Fiber = 4.4,
                                        Carbs = 25.1
                                    },
                                    Amount = 1
                                }
                            },
                            Instructions = new List<string>
                            {
                                "Preheat oven to 425 degrees F (220 degrees C).",
                                "Melt the butter in a saucepan. Stir in flour to form a paste. Add water, white sugar and brown sugar, and bring to a boil. Reduce temperature and let simmer.",
                                "Place the bottom crust in your pan. Fill with apples, mounded slightly. Cover with a lattice work crust. Gently pour the sugar and butter liquid over the crust. Pour slowly so that it does not run off.",
                                "Bake 15 minutes in the preheated oven. Reduce the temperature to 350 degrees F (175 degrees C). Continue baking for 35 to 45 minutes, until apples are soft."
                            },
                            Calories = 195,
                            Fat = 0.6,
                            Protein = 1,
                            Fiber = 8.8,
                            Carbs = 50.2
                        },
                        Amount = 1
                    }
                },
                Calories = 195,
                Fat = 0.6,
                Protein = 1,
                Fiber = 8.8,
                Carbs = 50.2
            },
            new()
            {
                Name = "Lunch",
                Recipes = new List<MealRecipeEntry>
                {
                    new()
                    {
                        Recipe = new RecipeModel
                        {
                            Name = "Carrot Cake",
                            Ingredients = new List<RecipeIngredientEntry>
                            {
                                new()
                                {
                                    Ingredient = new IngredientModel
                                    {
                                        Name = "Carrot",
                                        Calories = 95,
                                        Fat = 0.3,
                                        Protein = 0.5,
                                        Fiber = 4.4,
                                        Carbs = 25.1
                                    },
                                    Amount = 1
                                },
                                new()
                                {
                                    Ingredient = new IngredientModel
                                    {
                                        Name = "Cake",
                                        Calories = 100,
                                        Fat = 0.3,
                                        Protein = 0.5,
                                        Fiber = 4.4,
                                        Carbs = 25.1
                                    },
                                    Amount = 1
                                }
                            },
                            Instructions = new List<string>
                            {
                                "Preheat oven to 425 degrees F (220 degrees C).",
                                "Melt the butter in a saucepan. Stir in flour to form a paste. Add water, white sugar and brown sugar, and bring to a boil. Reduce temperature and let simmer.",
                                "Place the bottom crust in your pan. Fill with apples, mounded slightly. Cover with a lattice work crust. Gently pour the sugar and butter liquid over the crust. Pour slowly so that it does not run off.",
                                "Bake 15 minutes in the preheated oven. Reduce the temperature to 350 degrees F (175 degrees C). Continue baking for 35 to 45 minutes, until apples are soft."
                            },
                            Calories = 195,
                            Fat = 0.6,
                            Protein = 1,
                            Fiber = 8.8,
                            Carbs = 50.2
                        },
                        Amount = 1
                    }
                },
                Calories = 195,
                Fat = 0.6,
                Protein = 1,
                Fiber = 8.8,
                Carbs = 50.2
            }
        };

        return meals;
    }
    
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
        return NoContent();
    }
    
    // PUT api/Meals/{name}
    [HttpPut("{name}")]
    [Authorize]
    public IActionResult EditMeal(string name, CreateMealInfo info)
    {
        // TODO: Implement edit on the backend
        
        if (name != info.Name)
        {
            return BadRequest();
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

        return _app.FoodControl.ConsumeMeal(name, user.UserName) ?
            NoContent() : BadRequest("You don't have enough ingredients in stock");
    }
    
    // DELETE api/Meals/{name}
    [HttpDelete("{name}")]
    [Authorize]
    public async Task<IActionResult> DeleteMeal(string name)
    {
        // TODO: Implement delete on backend
        return NoContent();
    }
}