using System;
using NutriApp.Food;
using NutriApp.Undo;

namespace NutriApp.UI;

class CreateMealCommand : Command<Meal>
{
    private App _app;
    private Guid _sessionKey;

    public CreateMealCommand(App app, Guid sessionKey)
    {
        _app = app;
        _sessionKey = sessionKey;
    }

    public override void Execute(Meal meal)
    {
        _app.FoodControl.AddMeal(meal);

        UndoCommand undoCommand = new UndoCreateMeal(_app.FoodControl, meal);
        _app.UserControl.AddUndoCommand(_sessionKey, undoCommand);

        onFinished?.Invoke();
    }
}