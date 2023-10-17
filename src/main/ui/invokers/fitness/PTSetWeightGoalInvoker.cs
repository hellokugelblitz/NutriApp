using System;

namespace NutriApp;

class PTSetWeightGoalInvoker : CommandInvoker
{
    public PTSetWeightGoalInvoker(Command command) : base(command) { }

    public override void Invoke()
    {
        Console.WriteLine("Enter your weight: ");
        double weight = Convert.ToDouble(Console.ReadLine());
        command.Execute(weight);
    }
}