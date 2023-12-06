using System.Collections.Generic;
using System.Linq;

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

    public MealModel(Food.Meal meal)
    {
        Name = meal.Name;
        Recipes = meal.Children.Keys.Select(re => { return new RecipeModel(re); }).ToList();
        Calories = meal.Calories;
        Protein = meal.Protein;
        Fat = meal.Fat;
        Fiber = meal.Fiber;
        Carbs = meal.Carbohydrates;
    }
}