using System;
using NutriApp.Food;

namespace NutriApp.Undo;

public class UndoCreateRecipe : UndoCommand
{
    private App _app;
    private Recipe _recipe;

    public UndoCreateRecipe(App app, Recipe recipe)
    {
        _app = app;
        _recipe = recipe;
    }

    public override void Execute()
    {
        _app.FoodControl.RemoveRecipe(_recipe);

        onFinished?.Invoke();
    }
}