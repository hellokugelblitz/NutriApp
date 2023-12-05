using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using NutriApp.History;
using NutriApp.Save;

namespace NutriApp.Food;

/// <summary>
/// The most advanced food type, consisting of recipes.
/// </summary>
public class Meal : PreparedFood<Recipe>
{
    public override Dictionary<Ingredient, double> Ingredients
    {
        get
        {
            Dictionary<Ingredient, double> result = new Dictionary<Ingredient, double>();

            foreach (Recipe recipe in Children.Keys)
                // Concatenate recipe ingredient dictionary to result dictionary
                result = recipe.Children.Concat(result).ToDictionary(x => x.Key, x => x.Value);

            return result;
        }
    }

    public Meal(string name) : base(name) {}

    public override string ToString()
    {
        string output = Name + "(";

        foreach (var recipie in Children)
        {
            output += $"{{{recipie.Key.Name} : {recipie.Value}}}, ";
        }

        output += ")";
        return output;
    }

    public override bool Equals(object other)
    {
        Meal obj = other as Meal;
        return obj is not null && Name == obj.Name && Children.SequenceEqual(obj.Children);
    }
}