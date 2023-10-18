using System;
using System.Collections.Generic;
using NutriApp.Food;

namespace NutriApp.UI;

class PTPurchaseFoodInvoker : CommandInvoker<Array>
{
    private App app;

    public PTPurchaseFoodInvoker(Command<Array> command, App app) : base(command) 
    { 
        this.app = app;
    }

    public override void Invoke()
    {
        Console.WriteLine("Enter name of food to purchase: ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter quantity of the food you are going to purchase: ");
        int quantity = int.Parse(Console.ReadLine());

        command.Execute(new object[] { name, quantity });
    }
}