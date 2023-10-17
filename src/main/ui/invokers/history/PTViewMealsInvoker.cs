using System;

namespace NutriApp;

class PTViewMealsInvoker : CommandInvoker
{
    public PTViewMealsInvoker(Command command) : base(command) { }

    public override void Invoke()
    {
        command.Execute();
    }
}