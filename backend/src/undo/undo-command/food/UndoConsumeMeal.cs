using System;
using System.Linq;
using NutriApp.History;

namespace NutriApp.Undo;

class UndoConsumeMeal : UndoCommand
{
    private HistoryController _historyController;
    private string _mealName;
    private DateTime _timestamp;

    public UndoConsumeMeal(HistoryController historyController, string mealName, DateTime timestamp)
    {
        _mealName = mealName;
        _timestamp = timestamp;
        _historyController = historyController;
    }

    public override void Execute()
    {
        var mealEntry = _historyController.Meals
            .FirstOrDefault(entry => entry.TimeStamp == _timestamp && entry.Value.Equals(_mealName));
        _historyController.Meals.Remove(mealEntry);

        onFinished?.Invoke();
    }
}