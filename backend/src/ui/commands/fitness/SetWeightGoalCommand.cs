using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class SetWeightGoalCommand : Command<Goal.Goal>
{
    private App app;

    public SetWeightGoalCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Goal.Goal userinput)
    {
        app.GoalControl.Goal = userinput;
        onFinished?.Invoke();
    }
}