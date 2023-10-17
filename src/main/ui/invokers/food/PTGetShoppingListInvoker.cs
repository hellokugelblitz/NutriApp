using System;

namespace NutriApp;

class PTGetShoppingListInvoker : CommandInvoker
{
    public PTGetShoppingListInvoker(Command command) : base(command) { }

    public override void Invoke()
    {
        command.Execute();
    }
}