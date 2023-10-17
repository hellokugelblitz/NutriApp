using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class ViewTargetCaloriesCommand : Command<Goal.Goal>
{
    private App app;

    public ViewTargetCaloriesCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Goal.Goal userinput)
    {
        
    }
}