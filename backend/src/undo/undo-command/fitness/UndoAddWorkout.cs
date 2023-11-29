using System;
using System.Linq;
using NutriApp.History;

namespace NutriApp.Undo;

public class UndoAddWorkout : UndoCommand
{
    private HistoryController _historyController;
    private Workout.Workout _workout;
    private DateTime _timestamp;

    public UndoAddWorkout(HistoryController historyController, Workout.Workout workout, DateTime timestamp)
    {
        _historyController = historyController;
        _workout = workout;
        _timestamp = timestamp;
    }

    public override void Execute()
    {
        var workoutEntry = _historyController.Workouts
            .FirstOrDefault(entry => entry.TimeStamp == _timestamp && entry.Value.Equals(_workout));
        _historyController.Workouts.Remove(workoutEntry);
        
        onFinished?.Invoke();
    }
}
