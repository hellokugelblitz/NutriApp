using System;

namespace NutriApp.UI;

class PTClearHistoryInvoker : CommandInvoker<string>
{
    public PTClearHistoryInvoker(Command<string> command) : base(command) { }

    public override void Invoke()
    {

    }
}