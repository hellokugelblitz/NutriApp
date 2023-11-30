using System;
using System.Collections.Generic;
using System.Linq;

namespace NutriApp.Undo;

class UndoConsumeMeal : UndoCommand
{
    private App _app;
    private string _mealName;
    private DateTime _timestamp;

    public UndoConsumeMeal(App app, string mealName, DateTime timestamp)
    {
        _mealName = mealName;
        _timestamp = timestamp;
        _app = app;
    }

    public override void Execute()
    {
        var mealEntry = _app.HistoryControl.Meals
            .FirstOrDefault(entry => entry.TimeStamp == _timestamp && entry.Value.Equals(_mealName));
        _app.HistoryControl.Meals.Remove(mealEntry);

        var mealIngredients = mealEntry.Value.Ingredients;

        foreach (KeyValuePair<Food.Ingredient, double> kvp in mealIngredients)
        {
            _app.FoodControl.AddIngredientStock(kvp.Key.Name, kvp.Value);
        }

        onFinished?.Invoke();
    }
}