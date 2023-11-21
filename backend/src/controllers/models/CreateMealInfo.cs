using System.Collections.Generic;

namespace NutriApp.Controllers.Models;

public struct CreateMealInfo
{
    public string Name { get; set; }
    public Dictionary<string, int> Recipes { get; set; }
}