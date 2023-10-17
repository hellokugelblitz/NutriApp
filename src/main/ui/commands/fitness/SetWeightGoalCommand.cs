using System;

namespace NutriApp;

class SetWeightGoalCommand : Command
{
    private App app;

    public SetWeightGoalCommand(App app)
    {
        this.app = app;
    }

    public override void Execute()
    {
        
    }
}