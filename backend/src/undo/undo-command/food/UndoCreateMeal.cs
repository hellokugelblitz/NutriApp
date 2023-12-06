using System;
using NutriApp.Food;

namespace NutriApp.Undo;

public class UndoCreateMeal : UndoCommand
{
    private App _app;
    private Meal _meal;

    public UndoCreateMeal(App app, Meal meal) 
    {
        _app = app;
        _meal = meal;
    }

    public override void Execute()
    {
        _app.FoodControl.RemoveMeal(_meal);

        onFinished?.Invoke();
    }
}