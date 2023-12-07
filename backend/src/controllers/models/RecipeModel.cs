using System.Collections.Generic;
using System.Linq;
using NutriApp.Food;

namespace NutriApp.Controllers.Models;

public struct RecipeModel
{
    public string Name { get; set; }
    public List<RecipeIngredientEntry> Ingredients { get; set; }
    public List<string> Instructions { get; set; }
    public double Calories { get; set; }
    public double Fat { get; set; }
    public double Protein { get; set; }
    public double Fiber { get; set; }
    public double Carbs { get; set; }
    
    public static RecipeModel FromRecipe(Recipe recipe) => new RecipeModel
        {
            Name = recipe.Name,
            Ingredients = recipe.Ingredients
                .Select(pair => new RecipeIngredientEntry
                {
                    Ingredient = IngredientModel.FromIngredient(pair.Key),
                    Amount = pair.Value
                }).ToList(),
            Instructions = recipe.Instructions.ToList(),
            Calories = recipe.Calories,
            Fat = recipe.Fat,
            Protein = recipe.Protein,
            Fiber = recipe.Fiber,
            Carbs = recipe.Carbohydrates
        };
}

public struct RecipeIngredientEntry
{
    public IngredientModel Ingredient { get; set; }
    public double Amount { get; set; }
}