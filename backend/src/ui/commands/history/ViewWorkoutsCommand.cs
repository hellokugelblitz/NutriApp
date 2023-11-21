using System;

namespace NutriApp.UI;

class ViewWorkoutsCommand : Command<None>
{
    private App app;

    public ViewWorkoutsCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(None userinput)
    {
        onFinished?.Invoke();
    }
}