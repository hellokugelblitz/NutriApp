using System;
using NutriApp.Food;
using NutriApp.History;

namespace NutriApp.UI;

class PTViewMealsEatenUpdater : Updater
{
    public PTViewMealsEatenUpdater(ViewMealsEatenCommand viewMealsCommand, App app): base(app)
    {
        viewMealsCommand.Subscribe(Update);
    }
 
    public override void Update()
    {
        Console.WriteLine("View Meals");

        foreach (var meal in _app.HistoryControl.Meals)
        {
            PrintEntry(meal);
        }
    }

    private void PrintEntry(Entry<Meal> meal)
    {
        Console.WriteLine($"{meal.TimeStamp}| {meal.Value.ToString()}");
    }
}