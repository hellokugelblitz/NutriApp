using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTGetShoppingListInvoker : CommandInvoker<None>
{
    public PTGetShoppingListInvoker(Command<None> command) : base(command) { }

    public override void Invoke()
    {
        Console.WriteLine("Getting the Shopping List...");
        command.Execute(null);
    }
}