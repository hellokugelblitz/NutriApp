using System;

namespace NutriApp;

class PTViewWorkoutsInvoker : CommandInvoker
{
    public PTViewWorkoutsInvoker(Command command) : base(command) { }

    public override void Invoke()
    {
        command.Execute();
    }
}