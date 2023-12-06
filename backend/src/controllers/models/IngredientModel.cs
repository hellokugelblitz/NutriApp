namespace NutriApp.Controllers.Models;

public struct IngredientModel
{
    public string Name { get; set; }
    public double Calories { get; set; }
    public double Fat { get; set; }
    public double Protein { get; set; }
    public double Fiber { get; set; }
    public double Carbs { get; set; }
}