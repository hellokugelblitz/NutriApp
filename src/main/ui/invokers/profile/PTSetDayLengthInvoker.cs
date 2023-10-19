using System;

namespace NutriApp.UI;

class PTSetDayLengthInvoker : CommandInvoker<double>
{
    public PTSetDayLengthInvoker(Command<double> command) : base(command) { }

    public override void Invoke()
    {
        Console.WriteLine("Enter the new day length (minutes): ");
        double dayLength = double.Parse(Console.ReadLine());

        command.Execute(dayLength);
    }
}