using System;

namespace NutriApp.UI;

class ViewMealsEatenCommand : Command<None>
{
    private App app;

    public ViewMealsEatenCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(None userinput)
    {
        onFinished?.Invoke();
    }
}