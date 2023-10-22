using System;
using System.Collections.Generic;
using NutriApp.Food;
using NutriApp.History;

namespace NutriApp.UI;

class PTGetShoppingListUpdater : Updater
{
    public PTGetShoppingListUpdater(GetShoppingListCommand getShoppingListCommand, App app) : base(app)
    {
        getShoppingListCommand.Subscribe(Update);
    }

    public override void Update()
    {
        foreach (var entry in _app.FoodControl.ShoppingList.getList())
        {
            PrintEntry(entry);
        }
    }

    private void PrintEntry(KeyValuePair<Ingredient, double> entry) 
    {
        Console.WriteLine($"Ingredient: {entry.Key.Name} | Quantity: {entry.Value}");
    }
}