using System;

namespace NutriApp;

class SetFitnessGoalCommand<T> : Command<T>
{
    private App app;

    public SetFitnessGoalCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(T userinput)
    {
        
    }
}