using System;

namespace NutriApp.UI;

class QuitCommand : Command<None>
{
    private App _app;
    
    public QuitCommand(App app)
    {
        _app = app;
    }
    public override void Execute(None userinput)
    {
        _app.FoodControl.Save();
        _app.HistoryControl.Save();
        onFinished?.Invoke();// saves everything
        Environment.Exit(0);
    }
}