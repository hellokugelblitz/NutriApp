using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class SetFitnessGoalCommand : Command<Workout.Goal>
{
    private App app;

    public SetFitnessGoalCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Workout.Goal userinput)
    {
        
    }
}