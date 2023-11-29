using System;
using System.Linq;
using NutriApp.History;

namespace NutriApp.Undo;

class UndoSetWeight : UndoCommand
{
    private HistoryController _historyController;
    private double _weight;
    private DateTime _timestamp;

    public UndoSetWeight(HistoryController historyController, double weight, DateTime timestamp)
    {
        _historyController = historyController;
        _weight = weight;
        _timestamp = timestamp;
    }

    public override void Execute()
    {
        var weightEntry = _historyController.Weights
            .FirstOrDefault(entry => entry.TimeStamp == _timestamp && entry.Value.Equals(_weight));
        _historyController.Weights.Remove(weightEntry);
        
        onFinished?.Invoke();
    }
}