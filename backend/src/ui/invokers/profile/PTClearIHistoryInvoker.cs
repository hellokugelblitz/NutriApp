using System;

namespace NutriApp.UI;

class PTClearHistoryInvoker : CommandInvoker<None>
{
    public PTClearHistoryInvoker(Command<None> command) : base(command) { }

    public override void Invoke()
    {
        command.Execute(null);
    }
}