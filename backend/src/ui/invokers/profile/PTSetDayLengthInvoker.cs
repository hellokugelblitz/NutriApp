using System;

namespace NutriApp.UI;

class PTSetDayLengthInvoker : CommandInvoker<double>
{
    public PTSetDayLengthInvoker(Command<double> command) : base(command) { }

    public override void Invoke()
    {
        Console.WriteLine("Enter the new day length (minutes): ");
        double dayLength = -1;

        while (dayLength == -1)
        {
            try
            {
                dayLength = double.Parse(Console.ReadLine());
                if (dayLength <= 0)
                {
                    dayLength = -1;
                    Console.WriteLine("that is not a valid input");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("that is not a valid input");
            }
        }

        command.Execute(dayLength);
    }
}