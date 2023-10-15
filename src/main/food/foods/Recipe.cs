using System.Collections.Generic;

namespace NutriApp.Food;

public class Recipe : PreparedFood<Ingredient>
{
    private List<string> instructions;

    public string[] Instructions { get; }

    public void AddInstruction(string step) {}
}