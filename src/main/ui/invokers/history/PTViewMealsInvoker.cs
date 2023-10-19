using System;

namespace NutriApp.UI;

class PTViewMealsInvoker : CommandInvoker<string>
{
    public PTViewMealsInvoker(Command<string> command) : base(command) { }

    public override void Invoke()
    {

    }
}