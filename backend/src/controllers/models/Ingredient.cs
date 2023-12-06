namespace NutriApp.Controllers.Models;

public struct Ingredient
{
    public string Name { get; set; }
    public double Calories { get; set; }
    public double Fat { get; set; }
    public double Protein { get; set; }
    public double Fiber { get; set; }
    public double Carbs { get; set; }

    /// <summary>
    /// Takes an <see cref="Food.Ingredient"/> from the backend and
    /// converts it to an <see cref="Ingredient"/> for the frontend.
    /// </summary>
    /// <param name="ing">The ingredient to convert</param>
    /// <returns>Ingredient</returns>
    public static Ingredient FromIngredient(Food.Ingredient ing) => new()
    {
        Name = ing.Name,
        Calories = ing.Calories,
        Fat = ing.Fat,
        Protein = ing.Protein,
        Fiber = ing.Fiber,
        Carbs = ing.Carbohydrates
    };
}