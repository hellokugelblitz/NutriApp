namespace NutriApp.Food;

public class Ingredient : Food
{
    private string name;
    private double stock;

    public double Stock { get; set; }
    public string Name { get; }
    public double Calories { get; }
    public double Fat { get; }
    public double Protein { get; }
    public double Fiber { get; }
    public double Carbohydrates { get; }
}