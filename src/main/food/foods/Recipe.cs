using System.Collections.Generic;

namespace NutriApp.Food;

public class Recipe : PreparedFood<Ingredient>
{
    private List<string> instructions;

    public string[] Instructions { get; }

    public override Dictionary<Ingredient, double> Ingredients
    {
        get
        {
            Dictionary<Ingredient, double> results = new Dictionary<Ingredient, double>();

            foreach (Ingredient ingredient in Children.Keys)
                results.Add(ingredient, Children[ingredient]);

            return results;
        }
    }

    public Recipe(string name) : base(name) {}

    public void AddInstruction(string step) => instructions.Add(step);
}