using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class ViewTargetCaloriesCommand : Command<string>
{
    private App app;

    public ViewTargetCaloriesCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(string input)
    {
        onFinished?.Invoke();
    }
}