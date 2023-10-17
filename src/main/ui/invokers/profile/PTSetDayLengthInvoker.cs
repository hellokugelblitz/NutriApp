using System;

namespace NutriApp.UI;

class PTSetDayLengthInvoker : CommandInvoker<string>
{
    private App app;

    public PTSetDayLengthInvoker(Command<string> command, App app) : base(command) 
    { 
        this.app = app;
    }

    public override void Invoke()
    {

    }
}