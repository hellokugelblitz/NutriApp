namespace NutriApp.Food;

public class Meal : PreparedFood<Recipe>
{
    public Meal(string name) : base(name) {}
}