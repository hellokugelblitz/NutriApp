using System;
using NutriApp.Goal;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTSetWeightGoalInvoker : CommandInvoker<Goal.Goal>
{
    private App app;

    public PTSetWeightGoalInvoker(Command<Goal.Goal> command, App app) : base(command) 
    { 
        this.app = app;
    }

    public override void Invoke()
    {
        Console.WriteLine("please enter a target weight");
        double targetWeight = Double.Parse(Console.ReadLine());

        var goal = app.GoalControl.GetGoalBasedOnWeightDifference(targetWeight);

        command.Execute(goal);
    }
}