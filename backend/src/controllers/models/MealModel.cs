using System.Collections.Generic;
using System.Linq;
using NutriApp.Food;

namespace NutriApp.Controllers.Models;

public struct MealModel
{
    public string Name { get; set; }
    public List<MealRecipeEntry> Recipes { get; set; }
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Fat { get; set; }
    public double Fiber { get; set; }
    public double Carbs { get; set; }

    public static MealModel FromMeal(Meal meal) => new()
    {
        Name = meal.Name,
        Recipes = meal.Children
            .Select(pair => new MealRecipeEntry { Recipe = RecipeModel.FromRecipe(pair.Key), Amount = pair.Value  })
            .ToList(),
        Calories = meal.Calories,
        Protein = meal.Protein,
        Fat = meal.Fat,
        Fiber = meal.Fiber,
        Carbs = meal.Carbohydrates
    };
}

public struct MealRecipeEntry
{
    public RecipeModel Recipe { get; set; }
    public double Amount { get; set; }
}