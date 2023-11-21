using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;

namespace NutriApp.Controllers;

[Route("api/Recipes")]
[ApiController]
public class RecipesApiController : ControllerBase
{
    private readonly App _app;
    
    public RecipesApiController(App app)
    {
        _app = app;
    }
    
    // GET api/Recipes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
    {
        Recipe[] recipes =
        {
            // Some dummy recipe structs
            new()
            {
                Name = "Apple Pie",
                Ingredients = new List<Ingredient>
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
                        Name = "Sugar",
                        Calories = 387,
                        Fat = 0,
                        Protein = 0,
                        Fiber = 0,
                        Carbs = 100
                    },
                    new()
                    {
                        Name = "Flour",
                        Calories = 364,
                        Fat = 1.2,
                        Protein = 10.3,
                        Fiber = 3.4,
                        Carbs = 76
                    },
                    new()
                    {
                        Name = "Butter",
                        Calories = 717,
                        Fat = 81,
                        Protein = 0.9,
                        Fiber = 0,
                        Carbs = 0.1
                    }
                },
                Instructions = new List<string>
                {
                    "Preheat oven to 425 degrees F (220 degrees C). Melt the butter in a saucepan. Stir in flour to form a paste. Add water, white sugar and brown sugar, and bring to a boil. Reduce temperature and let simmer.",
                    "Place the bottom crust in your pan. Fill with apples, mounded slightly. Cover with a lattice work crust. Gently pour the sugar and butter liquid over the crust. Pour slowly so that it does not run off.",
                    "Bake 15 minutes in the preheated oven. Reduce the temperature to 350 degrees F (175 degrees C). Continue baking for 35 to 45 minutes, until apples are soft."
                },
            },
            new()
            {
                Name = "Banana Bread",
                Ingredients = new List<Ingredient>
                {
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
                        Name = "Sugar",
                        Calories = 387,
                        Fat = 0,
                        Protein = 0,
                        Fiber = 0,
                        Carbs = 100
                    },
                    new()
                    {
                        Name = "Flour",
                        Calories = 364,
                        Fat = 1.2,
                        Protein = 10.3,
                        Fiber = 3.4,
                        Carbs = 76
                    },
                    new()
                    {
                        Name = "Butter",
                        Calories = 717,
                        Fat = 81,
                        Protein = 0.9,
                        Fiber = 0,
                        Carbs = 0.1
                    }
                },
                Instructions = new List<string>
                {
                    "Preheat oven to 350 degrees F (175 degrees C). Lightly grease a 9x5 inch loaf pan.",
                    "In a large bowl, combine flour, baking soda and salt. In a separate bowl, cream together butter and brown sugar. Stir in eggs and mashed bananas until well blended. Stir banana mixture into flour mixture; stir just to moisten. Pour batter into prepared loaf pan.",
                    "Bake in preheated oven for 60 to 65 minutes, until a toothpick inserted into center of the loaf comes out clean. Let bread cool in pan for 10 minutes, then turn out onto a wire rack."
                },
            },
            new()
            {
                Name = "Orange Juice",
                Ingredients = new List<Ingredient>
                {
                    new()
                    {
                        Name = "Orange",
                        Calories = 45,
                        Fat = 0.1,
                        Protein = 0.9,
                        Fiber = 2.3,
                        Carbs = 11
                    },
                    new()
                    {
                        Name = "Sugar",
                        Calories = 387,
                        Fat = 0,
                        Protein = 0,
                        Fiber = 0,
                        Carbs = 100
                    },
                    new()
                    {
                        Name = "Water",
                        Calories = 0,
                        Fat = 0,
                        Protein = 0,
                        Fiber = 0,
                        Carbs = 0
                    }
                },
                Instructions = new List<string>
                {
                    "Cut the oranges in half and squeeze the juice into a measuring cup. You will need 1 cup of juice.",
                    "Pour the juice into a pitcher. Add 1 cup of water and stir. Taste the juice. If you want it to be a little sweeter, add 1 tablespoon of sugar and stir until it dissolves. Repeat until the juice is as sweet as you like it."
                },
            }
        };

        return recipes;
    }
    
    // GET api/Recipes/{name}
    [HttpGet("{name}")]
    public async Task<ActionResult<Recipe>> GetRecipe(string name)
    {
        Recipe recipe = new()
        {
            Name = name,
            Ingredients = new List<Ingredient>
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
                    Name = "Sugar",
                    Calories = 387,
                    Fat = 0,
                    Protein = 0,
                    Fiber = 0,
                    Carbs = 100
                },
                new()
                {
                    Name = "Flour",
                    Calories = 364,
                    Fat = 1.2,
                    Protein = 10.3,
                    Fiber = 3.4,
                    Carbs = 76
                },
                new()
                {
                    Name = "Butter",
                    Calories = 717,
                    Fat = 81,
                    Protein = 0.9,
                    Fiber = 0,
                    Carbs = 0.1
                }
            },
            Instructions = new List<string>
            {
                "Preheat oven to 425 degrees F (220 degrees C). Melt the butter in a saucepan. Stir in flour to form a paste. Add water, white sugar and brown sugar, and bring to a boil. Reduce temperature and let simmer.",
                "Place the bottom crust in your pan. Fill with apples, mounded slightly. Cover with a lattice work crust. Gently pour the sugar and butter liquid over the crust. Pour slowly so that it does not run off.",
                "Bake 15 minutes in the preheated oven. Reduce the temperature to 350 degrees F (175 degrees C). Continue baking for 35 to 45 minutes, until apples are soft."
            },
        };
        return recipe;
    }
    
    // GET api/Recipes/search/{name}
    [HttpGet("search/{name}")]
    public async Task<ActionResult<IEnumerable<Recipe>>> SearchRecipes(string name)
    {
        Recipe[] recipes =
        {
            new()
            {
                Name = "Apple Pie",
                Ingredients = new List<Ingredient>
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
                        Name = "Sugar",
                        Calories = 387,
                        Fat = 0,
                        Protein = 0,
                        Fiber = 0,
                        Carbs = 100
                    },
                    new()
                    {
                        Name = "Flour",
                        Calories = 364,
                        Fat = 1.2,
                        Protein = 10.3,
                        Fiber = 3.4,
                        Carbs = 76
                    },
                    new()
                    {
                        Name = "Butter",
                        Calories = 717,
                        Fat = 81,
                        Protein = 0.9,
                        Fiber = 0,
                        Carbs = 0.1
                    }
                },
                Instructions = new List<string>
                {
                    "Preheat oven to 425 degrees F (220 degrees C). Melt the butter in a saucepan. Stir in flour to form a paste. Add water, white sugar and brown sugar, and bring to a boil. Reduce temperature and let simmer.",
                    "Place the bottom crust in your pan. Fill with apples, mounded slightly. Cover with a lattice work crust. Gently pour the sugar and butter liquid over the crust. Pour slowly so that it does not run off.",
                    "Bake 15 minutes in the preheated oven. Reduce the temperature to 350 degrees F (175 degrees C). Continue baking for 35 to 45 minutes, until apples are soft."
                },
            },
            new()
            {
                Name = "Banana Bread",
                Ingredients = new List<Ingredient>
                {
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
                        Name = "Sugar",
                        Calories = 387,
                        Fat = 0,
                        Protein = 0,
                        Fiber = 0,
                        Carbs = 100
                    },
                    new()
                    {
                        Name = "Flour",
                        Calories = 364,
                        Fat = 1.2,
                        Protein = 10.3,
                        Fiber = 3.4,
                        Carbs = 76
                    },
                    new()
                    {
                        Name = "Butter",
                        Calories = 717,
                        Fat = 81,
                        Protein = 0.9,
                        Fiber = 0,
                        Carbs = 0.1
                    }
                },
                Instructions = new List<string>
                {
                    "Preheat oven to 350 degrees F (175 degrees C). Lightly grease a 9x5 inch loaf pan.",
                    "In a large bowl, combine flour, baking soda and salt. In a separate bowl, cream together butter and brown sugar. Stir in eggs and mashed bananas until well blended. Stir banana mixture into flour mixture; stir just to moisten. Pour batter into prepared loaf pan.",
                    "Bake in preheated oven for 60 to 65 minutes, until a toothpick inserted into center of the loaf comes out clean. Let bread cool in pan for 10 minutes, then turn out onto a wire rack."
                },
            },
        };
        return recipes;
    }
    
    // POST api/Recipes
    [HttpPost]
    public async Task<ActionResult<Recipe>> CreateRecipe(CreateRecipeInfo info)
    {
        Recipe recipe = new() { Name = info.Name };
        return CreatedAtAction("GetRecipe", new { name = recipe.Name }, recipe);
    }
    
    // PUT api/Recipes/{name}
    [HttpPut("{name}")]
    public async Task<IActionResult> EditRecipe(string name, CreateRecipeInfo info)
    {
        if (name != info.Name)
        {
            return BadRequest();
        }
        
        return NoContent();
    }
    
    // DELETE api/Recipes/{name}
    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteRecipe(string name)
    {
        return NoContent();
    }
}