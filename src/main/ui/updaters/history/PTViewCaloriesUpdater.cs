using System;
using NutriApp.History;

namespace NutriApp.UI;

class PTViewCaloriesUpdater : Updater
{
    public PTViewCaloriesUpdater(ViewCaloriesCommand viewCaloriesCommand, App app): base(app)
    {
        viewCaloriesCommand.Subscribe(Update);
    }
 
    public override void Update()
    {
        foreach (var calorie in _app.HistoryControl.Calories)
        {
            PrintEntry(calorie);
        }
    }

    private void PrintEntry(Entry<CalorieTracker> cal)
    {
        Console.WriteLine($"{cal.TimeStamp}| eaten calories: {cal.Value.ActualCalories}   target calories: {cal.Value.TargetCalories}");
    }
}