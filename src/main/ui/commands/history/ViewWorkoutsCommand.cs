using System;

namespace NutriApp;

class ViewWorkoutsCommand<T> : Command<T>
{
    private App app;

    public ViewWorkoutsCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(T userinput)
    {
        
    }
}