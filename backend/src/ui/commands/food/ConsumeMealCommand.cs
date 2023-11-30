using System;
using NutriApp.Undo;

namespace NutriApp.UI;

class ConsumeMealCommand : Command<string>
{
    private App _app;
    private Guid _sessionKey;

    public ConsumeMealCommand(App app, Guid sessionKey)
    {
        _app = app;
        _sessionKey = sessionKey;
    }

    public override void Execute(string userinput)
    {
        _app.FoodControl.ConsumeMeal(userinput);

        var timestamp = _app.TimeStamp;
        UndoCommand undoCommand = new UndoConsumeMeal(_app, userinput, timestamp);
        _app.UserControl.AddUndoCommand(_sessionKey, undoCommand);

        onFinished?.Invoke();
    }
}