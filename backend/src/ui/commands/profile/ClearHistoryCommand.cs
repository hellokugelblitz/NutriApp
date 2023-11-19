using System;

namespace NutriApp.UI;

class ClearHistoryCommand : Command<None>
{
    private App app;

    public ClearHistoryCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(None userinput)
    {
        app.HistoryControl.ClearHistory();
        onFinished?.Invoke();
    }
}