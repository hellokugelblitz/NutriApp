using System;

namespace NutriApp.UI;

class ViewCaloriesCommand : Command<None>
{
    private App app;

    public ViewCaloriesCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(None userinput)
    {
        onFinished?.Invoke();
    }
}