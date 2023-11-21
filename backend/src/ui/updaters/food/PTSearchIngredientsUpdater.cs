using System;

namespace NutriApp.UI;

class PTSearchIngredientsUpdater : Updater, SearchUpdater
{
    public PTSearchIngredientsUpdater(App app): base(app) { }

    public override void Update() { }

    public void DisplaySearch(string terms)
    {
        Console.WriteLine("Loading up ingredient...");
        foreach (var ingredient in _app.FoodControl.SearchIngredients(terms))
        {
            Console.WriteLine(ingredient);
        }

        _app.FoodControl.SearchIngredients(terms);
    }
}