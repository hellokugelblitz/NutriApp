using System;

namespace NutriApp.UI;

class PTSetDayLengthInvoker : CommandInvoker<double>
{
    public PTSetDayLengthInvoker(Command<double> command) : base(command) { }

    public override void Invoke()
    {

    }
}