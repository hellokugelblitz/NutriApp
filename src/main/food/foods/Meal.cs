using System.Collections.Generic;
using System.Linq;

namespace NutriApp.Food;

public class Meal : PreparedFood<Recipe>
{
    public Meal(string name) : base(name) {}

    public override Dictionary<Ingredient, double> Ingredients
    {
        get
        {
            Dictionary<Ingredient, double> results = new Dictionary<Ingredient, double>();

            foreach (Recipe recipe in Children.Keys)
                // Append recipe's ingredient dictionary to current dictionary to return
                results = results.Concat(recipe.Ingredients).ToDictionary(x => x.Key, x => x.Value);

            return results;
        }
    }
}