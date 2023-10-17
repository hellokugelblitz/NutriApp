using System;

namespace NutriApp.UI;

class PTViewWorkoutsInvoker : CommandInvoker<string>
{
    private App app;

    public PTViewWorkoutsInvoker(Command<string> command, App app) : base(command) 
    { 
        this.app = app;
    }

    public override void Invoke()
    {

    }
}