using System;

namespace NutriApp.UI;

class PTSetFitnessGoalUpdater : Updater
{
    public PTSetFitnessGoalUpdater(AddWorkoutCommand addWorkoutCommand, App app) : base(app)
    {
        addWorkoutCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine("Workout added successfully!");
    }
}