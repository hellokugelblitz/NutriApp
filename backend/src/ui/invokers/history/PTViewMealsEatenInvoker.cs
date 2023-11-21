using System;

namespace NutriApp.UI;

class PTViewMealsEatenInvoker : CommandInvoker<None>
{
    public PTViewMealsEatenInvoker(Command<None> command) : base(command) { }

    public override void Invoke()
    {
        command.Execute(null);
    }
}