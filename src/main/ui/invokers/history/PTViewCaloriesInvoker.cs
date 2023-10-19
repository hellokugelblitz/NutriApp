using System;

namespace NutriApp.UI;

class PTViewCaloriesInvoker : CommandInvoker<None>
{
    public PTViewCaloriesInvoker(Command<None> command) : base(command) { }

    public override void Invoke()
    {
        command.Execute(null);
    }
}