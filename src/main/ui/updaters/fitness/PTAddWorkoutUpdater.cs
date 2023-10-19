using System;

namespace NutriApp.UI;

class PTAddWorkoutUpdater : Updater
{
    public PTAddWorkoutUpdater(AddWorkoutCommand addWorkoutCommand, App app) : base(app)
    {
        addWorkoutCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine("Workout added successfully!");
    }
}