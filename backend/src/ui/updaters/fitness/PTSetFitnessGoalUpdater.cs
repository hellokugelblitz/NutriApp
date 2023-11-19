using System;

namespace NutriApp.UI;

class PTSetFitnessGoalUpdater : Updater
{
    public PTSetFitnessGoalUpdater(SetFitnessGoalCommand setFitnessGoalCommand, App app) : base(app)
    {
        setFitnessGoalCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine("Fitness Goal added successfully!");
    }
}