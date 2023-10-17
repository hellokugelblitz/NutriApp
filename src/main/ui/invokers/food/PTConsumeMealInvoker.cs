using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTConsumeMealInvoker : CommandInvoker<Meal>
{
    private App app;

    public PTConsumeMealInvoker(Command<Meal> command, App app) : base(command) 
    { 
        this.app = app;
    }

    public override void Invoke()
    {
        Console.WriteLine("Enter name of meal consumed: ");
        string name = Console.ReadLine();

        command.Execute(new Meal(name));
    }
}