using System;
using NutriApp.History;

namespace NutriApp.UI;

class PTViewWorkoutsUpdater : Updater
{
    public PTViewWorkoutsUpdater(ViewWorkoutsCommand viewWorkoutsCommand, App app): base(app)
    {
        viewWorkoutsCommand.Subscribe(Update);
    }
 
    public override void Update()
    {
        Console.WriteLine("View Workouts");

        foreach (var workout in _app.HistoryControl.Workouts)
        {
            PrintEntry(workout);
        }
    }

    private void PrintEntry(Entry<Workout.Workout> workout)
    {
        Console.WriteLine($"{workout.TimeStamp}| {workout.Value.ToString()}");
    }
}