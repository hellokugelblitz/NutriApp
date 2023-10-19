using System;

namespace NutriApp.UI;

class PTViewWorkoutsInvoker : CommandInvoker<None>
{
    public PTViewWorkoutsInvoker(Command<None> command) : base(command) { }

    public override void Invoke()
    {
        command.Execute(null);
    }
}