using System;
using System.Linq;
using NutriApp.History;

namespace NutriApp.Undo;

public class UndoAddWorkout : UndoCommand
{
    private App _app;
    private User _user;
    private DateTime _timestamp;

    public UndoAddWorkout(App app, User user, DateTime timestamp)
    {
        _app = app;
        _user = user;
        _timestamp = timestamp;
    }

    public override void Execute()
    {
        var workoutEntry = _app.HistoryControl.GetWorkouts(_user.Name)
            .FirstOrDefault(entry => entry.TimeStamp == _timestamp);
        _app.HistoryControl.GetWorkouts(_user.Name).Remove(workoutEntry);

        onFinished?.Invoke();
    }
}
