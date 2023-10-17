using System;

namespace NutriApp;

class PTCreateRecipeInvoker : CommandInvoker
{
    public PTCreateRecipeInvoker(Command command) : base(command) { }

    public override void Invoke()
    {
        command.Execute();   
    }
}