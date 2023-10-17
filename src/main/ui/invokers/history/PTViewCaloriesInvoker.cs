using System;

namespace NutriApp.UI;

class PTViewCaloriesInvoker : CommandInvoker<string>
{
    private App app;

    public PTViewCaloriesInvoker(Command<string> command, App app) : base(command) 
    { 
        this.app = app;
    }

    public override void Invoke()
    {

    }
}