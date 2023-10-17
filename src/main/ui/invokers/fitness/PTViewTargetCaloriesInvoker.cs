using System;

namespace NutriApp;

class PTViewTargetCaloriesInvoker : CommandInvoker
{
    public PTViewTargetCaloriesInvoker(Command command) : base(command) { }

    public override void Invoke()
    {
        command.Execute();
    }
}