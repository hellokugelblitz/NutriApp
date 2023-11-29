using System;
using NutriApp.UI;
using NutriApp.Undo;

namespace NutriApp.Commands;

class AddWorkoutCommand : Command<Workout.Workout>
{
    private App _app;
    private Guid _sessionKey;

    public AddWorkoutCommand(App app, Guid sessionKey)
    {
        _app = app;
        _sessionKey = sessionKey;
    }

    public override void Execute(Workout.Workout userinput)
    {

        _app.HistoryControl.AddWorkout(userinput);

        var timestamp = _app.TimeStamp;
        UndoAddWorkout undoCommand = new UndoAddWorkout(_app.HistoryControl, userinput, timestamp);
        _app.UserControl.AddUndoCommand(_sessionKey, undoCommand);

        onFinished?.Invoke();
    }
}
