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
        => _app.FoodControl.SearchRecipes(name)
            .Select(RecipeModel.FromRecipe).ToList();
    
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
}