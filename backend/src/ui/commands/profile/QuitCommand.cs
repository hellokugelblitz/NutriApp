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
        _app.GoalControl.Save();
        _app.Save();
        onFinished?.Invoke();
        Environment.Exit(0);
    }
}