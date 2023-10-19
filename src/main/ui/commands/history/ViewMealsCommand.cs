using System;

namespace NutriApp.UI;

class ViewMealsCommand : Command<None>
{
    private App app;

    public ViewMealsCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(None userinput)
    {
        onFinished?.Invoke();
    }
}