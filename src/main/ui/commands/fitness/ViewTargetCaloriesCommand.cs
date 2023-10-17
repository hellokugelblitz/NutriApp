using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class ViewTargetCaloriesCommand : Command<Workout.Goal>
{
    private App app;

    public ViewTargetCaloriesCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Workout.Goal userinput)
    {
        
    }
}