using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class ViewTargetCaloriesCommand : Command<None>
{
    private App app;

    public ViewTargetCaloriesCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(None input)
    {
        onFinished?.Invoke();
    }
}