using System;

namespace NutriApp;

class PTClearHistoryInvoker : CommandInvoker
{
    public PTClearHistoryInvoker(Command command) : base(command) { }

    public override void Invoke()
    {
        command.Execute();
    }
}