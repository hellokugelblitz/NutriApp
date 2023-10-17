using System;
using NutriApp.UI;

namespace NutriApp;

class PTAddWorkoutUpdater : Updater
{
    public PTAddWorkoutUpdater(AddWorkoutCommand addWorkoutCommand)
    {
        addWorkoutCommand.Subscribe(Update);
    }

    public void Update()
    {
        Console.WriteLine("Workout added successfully!");
    }
}