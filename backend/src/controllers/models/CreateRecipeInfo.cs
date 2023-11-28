using System.Collections.Generic;

namespace NutriApp.Controllers.Models;

public struct CreateRecipeInfo
{
    public string Name { get; set; }
    public List<string> Instructions { get; set; }
    public Dictionary<string, int> Ingredients { get; set; }
}