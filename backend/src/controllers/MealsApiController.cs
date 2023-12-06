using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;

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
    public async Task<ActionResult<IEnumerable<MealModel>>> GetMeals()
    {
        MealModel[] meals =
        {
            // Some dummy meals
            new()
            {
                Name = "Breakfast",
                Recipes = new List<RecipeModel>
                {
                    new()
                    {
                        Name = "Apple",
                        Calories = 95,
                        Fat = 0.3,
                        Protein = 0.5,
                        Fiber = 4.4,
                        Carbs = 25
                    },
                    new()
                    {
                        Name = "Banana",
                        Calories = 105,
                        Fat = 0.4,
                        Protein = 1.3,
                        Fiber = 3.1,
                        Carbs = 27
                    },
                    new()
                    {
                        Name = "Orange",
                        Calories = 45,
                        Fat = 0.1,
                        Protein = 0.9,
                        Fiber = 2.3,
                        Carbs = 11
                    }
                },
                Calories = 245,
                Fat = 0.8,
                Protein = 2.7,
                Fiber = 9.8,
                Carbs = 63
            },
            new()
            {
                Name = "Lunch",
                Recipes = new List<RecipeModel>
                {
                    new()
                    {
                        Name = "Apple",
                        Calories = 95,
                        Fat = 0.3,
                        Protein = 0.5,
                        Fiber = 4.4,
                        Carbs = 25
                    },
                    new()
                    {
                        Name = "Banana",
                        Calories = 105,
                        Fat = 0.4,
                        Protein = 1.3,
                        Fiber = 3.1,
                        Carbs = 27
                    },
                    new()
                    {
                        Name = "Orange",
                        Calories = 45,
                        Fat = 0.1,
                        Protein = 0.9,
                        Fiber = 2.3,
                        Carbs = 11
                    }
                },
                Calories = 245,
                Fat = 0.8,
                Protein = 2.7,
                Fiber = 9.8,
                Carbs = 63
            },
            new()
            {
                Name = "Dinner",
                Recipes = new List<RecipeModel>
                {
                    new()
                    {
                        Name = "Apple",
                        Calories = 95,
                        Fat = 0.3,
                        Protein = 0.5,
                        Fiber = 4.4,
                        Carbs = 25
                    },
                    new()
                    {
                        Name = "Banana",
                        Calories = 105,
                        Fat = 0.4,
                        Protein = 1.3,
                        Fiber = 3.1,
                        Carbs = 27
                    },
                    new()
                    {
                        Name = "Orange",
                        Calories = 45,
                        Fat = 0.1,
                        Protein = 0.9,
                        Fiber = 2.3,
                        Carbs = 11
                    }
                },
                Calories = 245,
                Fat = 0.8,
                Protein = 2.7,
                Fiber = 9.8,
                Carbs = 63
            }
        };
        
        return meals;
    }
    
    // GET api/Meals/{name}
    [HttpGet("{name}")]
    public async Task<ActionResult<MealModel>> GetMeal(string name)
    {
        MealModel mealModel = new()
        {
            Name = name,
            Recipes = new List<RecipeModel>
            {
                new()
                {
                    Name = "Apple",
                    Calories = 95,
                    Fat = 0.3,
                    Protein = 0.5,
                    Fiber = 4.4,
                    Carbs = 25
                },
                new()
                {
                    Name = "Banana",
                    Calories = 105,
                    Fat = 0.4,
                    Protein = 1.3,
                    Fiber = 3.1,
                    Carbs = 27
                },
                new()
                {
                    Name = "Orange",
                    Calories = 45,
                    Fat = 0.1,
                    Protein = 0.9,
                    Fiber = 2.3,
                    Carbs = 11
                }
            },
            Calories = 245,
            Fat = 0.8,
            Protein = 2.7,
            Fiber = 9.8,
            Carbs = 63
        };
        
        return mealModel;
    }
    
    // GET api/Meals/search/{name}
    [HttpGet("search/{name}")]
    public async Task<ActionResult<MealModel[]>> SearchMeals(string name)
    {
        MealModel[] meals =
        {
            // Some dummy meals
            new()
            {
                Name = "Breakfast",
                Recipes = new List<RecipeModel>
                {
                    new()
                    {
                        Name = "Apple",
                        Calories = 95,
                        Fat = 0.3,
                        Protein = 0.5,
                        Fiber = 4.4,
                        Carbs = 25
                    },
                    new()
                    {
                        Name = "Banana",
                        Calories = 105,
                        Fat = 0.4,
                        Protein = 1.3,
                        Fiber = 3.1,
                        Carbs = 27
                    },
                    new()
                    {
                        Name = "Orange",
                        Calories = 45,
                        Fat = 0.1,
                        Protein = 0.9,
                        Fiber = 2.3,
                        Carbs = 11
                    }
                },
                Calories = 245,
                Fat = 0.8,
                Protein = 2.7,
                Fiber = 9.8,
                Carbs = 63
            },
            new()
            {
                Name = "Lunch",
                Recipes = new List<RecipeModel>
                {
                    new()
                    {
                        Name = "Apple",
                        Calories = 95,
                        Fat = 0.3,
                        Protein = 0.5,
                        Fiber = 4.4,
                        Carbs = 25
                    },
                    new()
                    {
                        Name = "Banana",
                        Calories = 105,
                        Fat = 0.4,
                        Protein = 1.3,
                        Fiber = 3.1,
                        Carbs = 27
                    },
                    new()
                    {
                        Name = "Orange",
                        Calories = 45,
                        Fat = 0.1,
                        Protein = 0.9,
                        Fiber = 2.3,
                        Carbs = 11
                    }
                },
                Calories = 245,
                Fat = 0.8,
                Protein = 2.7,
                Fiber = 9.8,
                Carbs = 63
            },
            new()
            {
                Name = "Dinner",
                Recipes = new List<RecipeModel>
                {
                    new()
                    {
                        Name = "Apple",
                        Calories = 95,
                        Fat = 0.3,
                        Protein = 0.5,
                        Fiber = 4.4,
                        Carbs = 25
                    },
                    new()
                    {
                        Name = "Banana",
                        Calories = 105,
                        Fat = 0.4,
                        Protein = 1.3,
                        Fiber = 3.1,
                        Carbs = 27
                    },
                    new()
                    {
                        Name = "Orange",
                        Calories = 45,
                        Fat = 0.1,
                        Protein = 0.9,
                        Fiber = 2.3,
                        Carbs = 11
                    }
                },
                Calories = 245,
                Fat = 0.8,
                Protein = 2.7,
                Fiber = 9.8,
                Carbs = 63
            }
        };

        return meals;
    }
    
    // POST api/Meals
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<MealModel>> CreateMeal(CreateMealInfo info)
    {
        MealModel mealModel = new () { Name = info.Name };
        return CreatedAtAction(nameof(GetMeal), new { name = mealModel.Name }, mealModel);
    }
    
    // PUT api/Meals/{name}
    [HttpPut("{name}")]
    [Authorize]
    public async Task<IActionResult> EditMeal(string name, CreateMealInfo info)
    {
        if (name != info.Name)
        {
            return BadRequest();
        }

        return NoContent();
    }
    
    // POST api/Meals/consume/{name}
    [HttpPost("consume/{name}")]
    [Authorize]
    public async Task<IActionResult> ConsumeMeal(string name)
    {
        return NoContent();
    }
    
    // DELETE api/Meals/{name}
    [HttpDelete("{name}")]
    [Authorize]
    public async Task<IActionResult> DeleteMeal(string name)
    {
        return NoContent();
    }
}