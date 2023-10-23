using System;
using System.Collections.Generic;
using NutriApp.Food;

namespace NutriApp.UI;

class PTPurchaseFoodInvoker : CommandInvoker<(string, double)>
{
    private App _app;
    
    public PTPurchaseFoodInvoker(Command<(string, double)> command) : base(command) { }

    public override void Invoke()
    {
        Console.WriteLine("Enter name of food to purchase: ");
        string name = Console.ReadLine().ToLower();
        double quantity = -1;

        while (quantity == -1)
        {
            try
            {
                Console.WriteLine("Enter quantity of the food you are going to purchase: ");
                quantity = double.Parse(Console.ReadLine());
                if(quantity < 0) 
                {
                    quantity = -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("not a valid input");
            }

        }

        if (!_app.FoodControl.EnoughIngredients(name))
        {
            Console.WriteLine("not enough ingredients");
            return;
        }
        
        command.Execute((name, quantity));
    }
}