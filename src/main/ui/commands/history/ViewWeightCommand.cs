using System;

namespace NutriApp.UI;

class ViewWeightCommand : Command<None>
{
    private App app;

    public ViewWeightCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(None userinput)
    {
        onFinished?.Invoke();
    }
}