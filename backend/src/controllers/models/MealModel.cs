using System.Collections.Generic;

namespace NutriApp.Controllers.Models;

public struct MealModel
{
    public string Name { get; set; }
    public List<RecipeModel> Recipes { get; set; }
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Fat { get; set; }
    public double Fiber { get; set; }
    public double Carbs { get; set; }
}