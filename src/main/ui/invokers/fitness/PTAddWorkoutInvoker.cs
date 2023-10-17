using System;

namespace NutriApp;

class PTAddWorkoutInvoker : CommandInvoker
{
    public PTAddWorkoutInvoker(Command command) : base(command) { }

    public override void Invoke()
    {
        command.Execute();
    }
}