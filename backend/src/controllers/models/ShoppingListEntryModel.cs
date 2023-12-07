using System.Collections.Generic;
using NutriApp.Food;

namespace NutriApp.Controllers.Models;

public struct ShoppingListEntryModel
{
    public string IngredientName { get; set; }
    public double Amount { get; set; }
    
    public static ShoppingListEntryModel FromEntry(KeyValuePair<Ingredient, double> entry) => new()
    {
        IngredientName = entry.Key.Name,
        Amount = entry.Value
    };
}