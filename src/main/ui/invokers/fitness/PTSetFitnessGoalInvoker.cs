using System;
using NutriApp.Goal;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTSetFitnessGoalInvoker : CommandInvoker<string>
{
    public PTSetFitnessGoalInvoker(Command<string> command) : base(command) 
    {
    }

    public override void Invoke()
    {
       Console.WriteLine("Do you want to incorporate fitness into your plan(yes, no)");
       string input = Console.ReadLine().ToLower().Trim();
       command.Execute(input);
    }
}