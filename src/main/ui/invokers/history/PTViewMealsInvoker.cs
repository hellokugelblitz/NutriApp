using System;

namespace NutriApp.UI;

class PTViewMealsInvoker : CommandInvoker<None>
{
    public PTViewMealsInvoker(Command<None> command) : base(command) { }

    public override void Invoke()
    {
        command.Execute(null);
    }
}