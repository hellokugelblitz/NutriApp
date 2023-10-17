using System;
using NutriApp.Goal;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTSetFitnessGoalInvoker : CommandInvoker<string>
{
    private App app;

    public PTSetFitnessGoalInvoker(Command<string> command, App app) : base(command) 
    {
        this.app = app;
    }

    public override void Invoke()
    {
       Console.WriteLine("Do you want to incorporate fitness into your plan(yes, no)");
       string input = Console.ReadLine();
       command.Execute(input);
    }
}