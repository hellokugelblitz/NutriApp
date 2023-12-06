using System.Collections.Generic;

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
}