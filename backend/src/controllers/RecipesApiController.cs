using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Models;
using NutriApp;
using NutriApp.Food;

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
    public ActionResult<IEnumerable<RecipeModel>> GetRecipes() 
        => _app.FoodControl.Recipes.Select(RecipeModel.FromRecipe).ToList();
    
    // GET api/Recipes/{name}
    [HttpGet("{name}")]
    public ActionResult<RecipeModel> GetRecipe(string name)
    {
        var recipe = _app.FoodControl.GetRecipe(name);
        return recipe != null ? RecipeModel.FromRecipe(recipe) : new RecipeModel();
    }
    
    // GET api/Recipes/search/{name}
    [HttpGet("search/{name}")]
    public ActionResult<IEnumerable<RecipeModel>> SearchRecipes(string name)
    {
        // TODO: Implement search on backend
        
        RecipeModel[] recipes =
        {
            new()
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
                            Carbs = 25
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
                            Carbs = 25
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
                Carbs = 50
            },
            new()
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
                            Carbs = 25
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
                            Carbs = 25
                        },
                        Amount = 1
                    }
                },
                Instructions = new List<string>
                {
                    "Preheat oven to 350 degrees F (175 degrees C). Grease and flour a 9x13 inch pan.",
                    "In a large bowl, beat together eggs, oil, white sugar and 2 teaspoons vanilla. Mix in flour, baking soda, baking powder, salt and cinnamon. Stir in carrots. Fold in pecans. Pour into prepared pan.",
                    "Bake in the preheated oven for 40 to 50 minutes, or until a toothpick inserted into the center of the cake comes out clean. Let cool in pan for 10 minutes, then turn out onto a wire rack and cool completely.",
                    "To Make Frosting: In a medium bowl, combine butter, cream cheese, confectioners' sugar and 1 teaspoon vanilla. Beat until the mixture is smooth and creamy. Stir in chopped pecans. Frost the cooled cake."
                },
                Calories = 195,
                Fat = 0.6,
                Protein = 1,
                Fiber = 8.8,
                Carbs = 50
            },
            new()
            {
                Name = "Chicken Pot Pie",
                Ingredients = new List<RecipeIngredientEntry>
                {
                    new()
                    {
                        Ingredient = new IngredientModel
                        {
                            Name = "Chicken",
                            Calories = 95,
                            Fat = 0.3,
                            Protein = 0.5,
                            Fiber = 4.4,
                            Carbs = 25
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
                            Carbs = 25
                        },
                        Amount = 1
                    }
                },
                Instructions = new List<string>
                {
                    "Preheat oven to 425 degrees F (220 degrees C.)",
                    "In a saucepan, combine chicken, carrots, peas, and celery. Add water to cover and boil for 15 minutes. Remove from heat, drain and set aside.",
                    "In the saucepan over medium heat, cook onions in butter until soft and translucent. Stir in flour, salt, pepper, and celery seed. Slowly stir in chicken broth and milk. Simmer over medium-low heat until thick. Remove from heat and set aside.",
                    "Place the chicken mixture in bottom pie crust. Pour hot liquid mixture over. Cover with top crust, seal edges, and cut away excess dough. Make several small slits in the top to allow steam to escape.",
                    "Bake in the preheated oven for 30 to 35 minutes, or until pastry is golden brown and filling is bubbly. Cool for 10 minutes before serving."
                },
                Calories = 195,
                Fat = 0.6,
                Protein = 1,
                Fiber = 8.8,
                Carbs = 50
            }
        };
        
        return recipes;
    }
    
    // POST api/Recipes
    [HttpPost]
    [Authorize]
    public ActionResult<RecipeModel> CreateRecipe(CreateRecipeInfo info)
    {
        if (_app.FoodControl.GetRecipe(info.Name) != null)
            return BadRequest("Recipe already exists; use PUT to edit");
        
        var recipe = new Recipe(info.Name);
        var badIngredients = new List<string>();
        
        foreach (var instruction in info.Instructions)
        {
            recipe.AddInstruction(instruction);
        }
        foreach (var (ingName, amount) in info.Ingredients)
        {
            var ing = _app.FoodControl.GetIngredient(ingName);
            if (ing == null)
                badIngredients.Add(ingName);
            else
                recipe.AddChild(ing, amount);
        }
        
        if (badIngredients.Count > 0)
            return BadRequest(new { message = "Some ingredients don't exist", badIngredients });
        
        _app.FoodControl.AddRecipe(recipe);
        return NoContent();
    }
    
    // PUT api/Recipes/{name}
    [HttpPut("{name}")]
    [Authorize]
    public IActionResult EditRecipe(string name, CreateRecipeInfo info)
    {
        // TODO: Implement edit on backend
        if (name != info.Name)
        {
            return BadRequest();
        }
        
        return NoContent();
    }
    
    // DELETE api/Recipes/{name}
    [HttpDelete("{name}")]
    [Authorize]
    public IActionResult DeleteRecipe(string name)
    {
        // TODO: Implement delete on backend
        return NoContent();
    }
}