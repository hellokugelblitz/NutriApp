using System;

namespace NutriApp;

class ViewWeightCommand<T> : Command<T>
{
    private App app;

    public ViewWeightCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(T userinput)
    {
        
    }
}