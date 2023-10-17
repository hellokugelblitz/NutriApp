using System;

namespace NutriApp;

class SetFitnessGoalCommand : Command
{
    private App app;

    public SetFitnessGoalCommand(App app)
    {
        this.app = app;
    }

    public override void Execute()
    {
        
    }
}