using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTCreateMealInvoker : CommandInvoker<Meal>
{
    private App _app;
    public PTCreateMealInvoker(Command<Meal> command, App app) : base(command)
    {
        _app = app;
    }
    public override void Invoke()
    {
        Console.WriteLine("Enter the name of the meal you want to create: ");
        string name = Console.ReadLine().ToLower();

        Meal meal = new Meal(name);

        Console.WriteLine("Enter name of the recipe you want in your meal. Type 'done' for recipe when you're finished.");
        while (true)
        {
            try { 
                Console.Write("Recipe: ");
                string recipe = Console.ReadLine().ToLower();

                if (recipe.ToLower() == "done") break;
            
                Console.Write("Amount: ");
                double amount = double.Parse(Console.ReadLine());

                meal.AddChild(_app.FoodControl.GetRecipe(recipe), amount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("not a valid input");
            }
            
        }

        command.Execute(meal);
        
        Console.WriteLine("created " + meal);
    }
}