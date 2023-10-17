using System;

namespace NutriApp;

class PTSetDayLengthInvoker : CommandInvoker
{
    public PTSetDayLengthInvoker(Command command) : base(command) { }

    public override void Invoke()
    {
        command.Execute();
    }
}