using System;

namespace NutriApp;

class SetWeightGoalCommand<T> : Command<T>
{
    private App app;

    public SetWeightGoalCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(T userinput)
    {
        
    }
}