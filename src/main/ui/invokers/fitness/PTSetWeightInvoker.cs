using System;

namespace NutriApp.UI;

class PTSetWeightInvoker : CommandInvoker<double>
{
    public PTSetWeightInvoker(Command<double> command) : base(command) { }
    public override void Invoke()
    {
        double weight = -1;

        while (weight == -1)
        {
            try
            {
                Console.WriteLine("please enter your weight for the day");
                weight = double.Parse(Console.ReadLine());
                if (weight <= 0)
                {
                    weight = -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("that was not a valid input");
            }
        }
        
        command.Execute(weight);
    }
}