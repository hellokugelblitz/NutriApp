using System;

namespace NutriApp.UI;

class PTSetDayLengthInvoker : CommandInvoker<string>
{
    public PTSetDayLengthInvoker(Command<string> command) : base(command) { }

    public override void Invoke()
    {

    }
}