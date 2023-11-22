using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;

namespace NutriApp.Controllers;

[Route("api/Ingredients")]
[ApiController]
public class IngredientsApiController : ControllerBase
{
    private readonly App _app;

    public IngredientsApiController(App app)
    {
        _app = app;
    }
    
    // GET api/Ingredients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
    {
        Ingredient[] ings =
        {
            // Some dummy ingredient structs
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
        };
        return ings;
    }
    
    // GET api/Ingredients/{name}
    [HttpGet("{name}")]
    public async Task<ActionResult<Ingredient>> GetIngredient(string name)
    {
        Ingredient ing = new()
        {
            Name = name,
            Calories = 95,
            Fat = 0.3,
            Protein = 0.5,
            Fiber = 4.4,
            Carbs = 25
        };
        return ing;
    }
    
    // GET api/Ingredients/search/{name}
    [HttpGet("search/{name}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> SearchIngredients(string name)
    {
        Ingredient[] ings =
        {
            // Some dummy ingredient structs that share similar names
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
                Name = "Apple Cinnamon",
                Calories = 105,
                Fat = 0.4,
                Protein = 1.3,
                Fiber = 3.1,
                Carbs = 27
            },
        };

        return ings;
    }
    
    // PUT api/Ingredients/purchase
    [HttpPut("purchase")]
    [Authorize]
    public async Task<IActionResult> PurchaseIngredients(string name, PurchaseIngredientInfo[] info)
    {
        return NoContent();
    }
}