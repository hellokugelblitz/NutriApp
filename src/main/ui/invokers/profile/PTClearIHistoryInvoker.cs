using System;

namespace NutriApp.UI;

class PTClearHistoryInvoker : CommandInvoker<string>
{

    private App app;

    public PTClearHistoryInvoker(Command<string> command, App app) : base(command) 
    { 
        this.app = app;
    }

    public override void Invoke()
    {

    }
}