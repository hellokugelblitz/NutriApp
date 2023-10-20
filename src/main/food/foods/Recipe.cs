using System.Collections.Generic;

namespace NutriApp.Food;

/// <summary>
/// An intermediate food type, consisting of ingredients.
/// </summary>
public class Recipe : PreparedFood<Ingredient>
{
    private List<string> instructions = new ();

    public string[] Instructions { get; }

    public Recipe(string name) : base(name) {}

    /// <summary>
    /// Adds a new preparation instruction for this recipe.
    /// </summary>
    public void AddInstruction(string step) => instructions.Add(step);
}