using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTConsumeMealInvoker : CommandInvoker<string>
{
    private App app;

    public PTConsumeMealInvoker(Command<string> command, App app) : base(command)
    {
        this.app = app;
    }
    
    public override void Invoke()
    {
        Console.WriteLine("Enter name of meal consumed: ");
        string name = Console.ReadLine();

        if (app.FoodControl.GetMeal(name) == null)
        {
            Console.WriteLine("Meal not found.");
            return;
        }

        if (!app.FoodControl.EnoughIngredients(name))
        {
            Console.WriteLine("Not enough ingredients to prepare this meal. Please consult the shopping list.");
            return;
        }

        command.Execute(name);
    }
}