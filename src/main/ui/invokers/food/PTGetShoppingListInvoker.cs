using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTGetShoppingListInvoker : CommandInvoker<string>
{
    public PTGetShoppingListInvoker(Command<string> command) : base(command) { }

    public override void Invoke()
    {
        Console.WriteLine("Getting the Shopping List...");
        command.Execute("");
    }
}