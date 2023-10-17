using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTGetShoppingListInvoker : CommandInvoker<string>
{
    private App app;

    public PTGetShoppingListInvoker(Command<string> command, App app) : base(command) 
    { 
        this.app = app;
    }

    public override void Invoke()
    {
        Console.WriteLine("Getting the Shopping List...");
        command.Execute("");
    }
}