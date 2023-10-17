using System.Collections.Generic;

namespace NutriApp.Food;

public class Recipe : PreparedFood<Ingredient>
{
    private List<string> instructions;

    public string[] Instructions { get; }

    public Recipe(string name) : base(name) {}

    public void AddInstruction(string step) => instructions.Add(step);
}