using System.Collections.Generic;
using Newtonsoft.Json;
using NutriApp.Controllers;

namespace NutriApp.Food;

/// <summary>
/// An intermediate food type, consisting of ingredients.
/// </summary>
public class Recipe : PreparedFood<Ingredient>
{
    private List<string> instructions = new ();

    public string[] Instructions => instructions.ToArray();

    public override Dictionary<Ingredient, double> Ingredients => Children;

    public Recipe(string name) : base(name)
    {
        instructions = new List<string>();
    }

    /// <summary>
    /// Adds a new preparation instruction for this recipe.
    /// </summary>
    public void AddInstruction(string step) => instructions.Add(step);

    public override string ToString()
    {
        string output = Name + "(";

        foreach (var ingredient in Children)
        {
            output += $" {{{ingredient.Key.Name} : {ingredient.Value},";
        }

        output += ") \n Instructions:\n";

        foreach (var instruction in instructions)
        {
            output += " " + instruction + "\n";
        }

        return output;
    }
}

public class SerializableRecipe : SerializablePreparedFood
{
    [JsonProperty] private string[] preparationInstructions;

    [JsonIgnore] public string[] Instructions => preparationInstructions;

    public SerializableRecipe(string name, string[] instructions) : base(name)
    {
        this.preparationInstructions = instructions;
    }
}