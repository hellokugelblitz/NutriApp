using System.Collections.Generic;
using System.Linq;

namespace NutriApp.Controllers.Models;

public struct RecipeModel
{
    public string Name { get; set; }
    public List<IngredientModel> Ingredients { get; set; }
    public List<string> Instructions { get; set; }
    public double Calories { get; set; }
    public double Fat { get; set; }
    public double Protein { get; set; }
    public double Fiber { get; set; }
    public double Carbs { get; set; }

    public RecipeModel(Food.Recipe recipe)
    {
        Name = recipe.Name;
        Ingredients = recipe.Ingredients.Keys.Select(re => { return new IngredientModel(re); }).ToList();
        Instructions = recipe.Instructions.ToList();
        Calories = recipe.Calories;
        Fat = recipe.Fat;
        Fiber = recipe.Fiber;
        Protein = recipe.Protein;
        Carbs = recipe.Carbohydrates;
    }
}