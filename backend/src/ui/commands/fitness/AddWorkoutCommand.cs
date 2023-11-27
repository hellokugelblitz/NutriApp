using System;
using NutriApp.History;
using NutriApp.UI;
using NutriApp.Undo;
using NutriApp.Workout;

namespace NutriApp.Commands;

class AddWorkoutCommand : Command<Workout.Workout>
{
    private readonly App app;
    private readonly Guid sessionKey;

    public AddWorkoutCommand(App app, Guid sessionKey)
    {
        this.app = app;
        this.sessionKey = sessionKey;
    }

    public override void Execute(Workout.Workout userinput)
    {

        app.HistoryControl.AddWorkout(userinput);

        var timestamp = app.TimeStamp;
        UndoAddWorkout undoCommand = new UndoAddWorkout(app.HistoryControl, userinput, timestamp);

        app.UserControl.AddUndoCommand(sessionKey, undoCommand);

        onFinished?.Invoke();
    }
}
