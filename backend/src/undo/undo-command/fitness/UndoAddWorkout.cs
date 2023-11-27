using System;
using System.Linq;
using NutriApp.History;

namespace NutriApp.Undo;

public class UndoAddWorkout : UndoCommand
{
    private readonly HistoryController _historyController;
    private readonly Workout.Workout _workout;
    private readonly DateTime _timestamp;

    public UndoAddWorkout(HistoryController historyController, Workout.Workout workout, DateTime timestamp)
    {
        _historyController = historyController;
        _workout = workout;
        _timestamp = timestamp;
    }

    public override void Execute()
    {
        // Find and remove the workout entry from the history
        var workoutEntry = _historyController.Workouts
            .FirstOrDefault(entry => entry.TimeStamp == _timestamp && entry.Value.Equals(_workout));
        
        if (workoutEntry != null)
        {
            _historyController.Workouts.Remove(workoutEntry);
        }
    }
}
