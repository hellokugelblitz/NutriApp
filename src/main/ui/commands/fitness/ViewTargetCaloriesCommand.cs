using System;

namespace NutriApp;

class ViewTargetCaloriesCommand<T> : Command<T>
{
    private App app;

    public ViewTargetCaloriesCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(T userinput)
    {
        
    }
}