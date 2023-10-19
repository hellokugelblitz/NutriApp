using System;
using System.Collections.Generic;
using NutriApp.Food;

namespace NutriApp.UI;

class PTPurchaseFoodInvoker : CommandInvoker<Array>
{
    public PTPurchaseFoodInvoker(Command<Array> command) : base(command) { }

    public override void Invoke()
    {
        Console.WriteLine("Enter name of food to purchase: ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter quantity of the food you are going to purchase: ");
        int quantity = int.Parse(Console.ReadLine());

        command.Execute(new object[] { name, quantity });
    }
}