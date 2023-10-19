using System;

namespace NutriApp.UI;

class PTViewWeightInvoker : CommandInvoker<None>
{
    public PTViewWeightInvoker(Command<None> command) : base(command) { }

    public override void Invoke()
    {
        command.Execute(null);
    }
}