using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTCreateRecipesInvoker : CommandInvoker<Recipe>
{
    private App _app;

    public PTCreateRecipesInvoker(Command<Recipe> command, App app) : base(command)
    {
        _app = app;
    }

    public override void Invoke()
    {
        Console.WriteLine("Enter the name of the recipe you want to create: ");
        string name = Console.ReadLine().ToLower();

        Recipe recipe = new Recipe(name);

        Console.WriteLine("Enter instructions for the recipe. Type 'done' when you're finished.");
        while (true)
        {
            Console.Write("Instruction: ");
            string instruction = Console.ReadLine().ToLower();

            if (instruction.ToLower() == "done") break;

            recipe.AddInstruction(instruction);
        }
        
        Console.WriteLine("Enter food for the recipe. Type 'done' for food when you're finished.");
        while (true)
        {
            try { 
                Console.Write("Food: ");
                string food = Console.ReadLine().ToLower();

                if (food.ToLower() == "done") break;
            
                Console.Write("Amount: ");
                double amount = double.Parse(Console.ReadLine());

                recipe.AddChild(_app.FoodControl.GetIngredient(food), amount);
            }
            catch (Exception e)
            {
                Console.WriteLine("not a valid input");
            }
            
        }

        command.Execute(recipe);
    }

}