using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTCreateRecipesInvoker : CommandInvoker<Recipe>
{
    private App app;

    public PTCreateRecipesInvoker(Command<Recipe> command, App app) : base(command)
    {
        this.app = app;
    }

    public override void Invoke()
    {
        Console.WriteLine("Enter the name of the recipe you want to create: ");
        string name = Console.ReadLine();

        Recipe recipe = new Recipe(name);

        Console.WriteLine("Enter instructions for the recipe. Type 'done' when you're finished.");
        while (true)
        {
            Console.Write("Instruction: ");
            string instruction = Console.ReadLine();

            if (instruction.ToLower() == "done") break;

            recipe.AddInstruction(instruction);
        }

        command.Execute(recipe);
    }

}