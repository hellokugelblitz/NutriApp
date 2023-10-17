using System;

namespace NutriApp;

class PTPurchaseFoodInvoker : CommandInvoker
{
    public PTPurchaseFoodInvoker(Command command) : base(command) { }

    public override void Invoke()
    {
        command.Execute();
    }
}