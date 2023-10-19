using System;

namespace NutriApp.UI;

class PTSetWeightGoalUpdater : Updater
{
    public PTSetWeightGoalUpdater(AddWorkoutCommand addWorkoutCommand, App app): base(app)
    {
        addWorkoutCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine("Workout added successfully!");
    }
}