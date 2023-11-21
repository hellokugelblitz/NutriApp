namespace NutriApp.Food;

/// <summary>
/// A generic type of food.
/// </summary>
public interface Food
{
    public string Name { get; }
    public double Calories { get; }
    public double Fat { get; }
    public double Protein { get; }
    public double Fiber { get; }
    public double Carbohydrates { get; }
}