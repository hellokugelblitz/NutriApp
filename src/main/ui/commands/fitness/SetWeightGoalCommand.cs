using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class SetWeightGoalCommand : Command<Workout.Goal>
{
    private App app;

    public SetWeightGoalCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Workout.Goal userinput)
    {
        
    }
}