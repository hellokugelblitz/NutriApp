using System;

namespace NutriApp.UI;

class PTViewMealsInvoker : CommandInvoker<string>
{
    private App app;

    public PTViewMealsInvoker(Command<string> command, App app) : base(command) 
    { 
        this.app = app;
    }

    public override void Invoke()
    {

    }
}