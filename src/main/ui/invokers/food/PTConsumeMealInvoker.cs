using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTConsumeMealInvoker : CommandInvoker<string>
{

    public PTConsumeMealInvoker(Command<string> command) : base(command) { }
    
    public override void Invoke()
    {
        Console.WriteLine("Enter name of meal consumed: ");
        string name = Console.ReadLine().ToLower();

        command.Execute(name);
    }
}