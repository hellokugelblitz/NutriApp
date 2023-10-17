using System;

namespace NutriApp.UI;

class PTViewWeightInvoker : CommandInvoker<string>
{
    private App app;

    public PTViewWeightInvoker(Command<string> command, App app) : base(command) 
    {
        this.app = app;
    }

    public override void Invoke()
    {

    }
}