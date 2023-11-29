using System;
using NutriApp.Undo;

namespace NutriApp.UI;

class SetWeightCommand : Command<double>
{
    private App _app;
    private Guid _sessionKey;
    
    public SetWeightCommand(App app, Guid sessionKey)
    {
        _app = app;
        _sessionKey = sessionKey;
    }
    
    public override void Execute(double userinput)
    {
        _app.HistoryControl.SetWeight(userinput);
        _app.GoalControl.CompareUserWeightToGoal();

        var timestamp = _app.TimeStamp;
        UndoCommand undoCommand = new UndoSetWeight(_app.HistoryControl, userinput, timestamp);
        _app.UserControl.AddUndoCommand(_sessionKey, undoCommand);

        onFinished?.Invoke();
    }
}