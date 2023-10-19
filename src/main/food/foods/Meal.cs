namespace NutriApp.Food;

/// <summary>
/// The most advanced food type, consisting of recipes.
/// </summary>
public class Meal : PreparedFood<Recipe>
{
    public Meal(string name) : base(name) {}
}