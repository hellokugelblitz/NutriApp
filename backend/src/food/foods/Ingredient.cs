namespace NutriApp.Food;

/// <summary>
/// The most basic type of food.
/// </summary>
public class Ingredient : Food
{
    private string name;

    private readonly double calories;
    private readonly double fat;
    private readonly double protein;
    private readonly double fiber;
    private readonly double carbohydrates;

    public string Name => name;
    public double Calories => calories;
    public double Fat => fat;
    public double Protein => fat;
    public double Fiber => fiber;
    public double Carbohydrates => carbohydrates;

    public Ingredient(string name, double calories, double fat, double protein, double fiber, double carbohydrates)
    {
        this.name = name;
        this.calories = calories;
        this.fat = fat;
        this.protein = protein;
        this.fiber = fiber;
        this.carbohydrates = carbohydrates;
    }

    public override string ToString() => name;
}