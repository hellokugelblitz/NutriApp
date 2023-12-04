using System;
using System.Collections.Generic;
using System.Linq;

namespace NutriApp.Undo;

class UndoConsumeMeal : UndoCommand
{
    private App _app;
    private User _user;
    private string _mealName;

    public UndoConsumeMeal(App app, User user, string mealName)
    {
        _app = app;
        _user = user;
        _mealName = mealName;
    }

    public override void Execute()
    {
        var meals = _app.HistoryControl.GetMeals(_user.Name);
        meals.RemoveAt(meals.Count - 1);

        var mealIngredients = _app.FoodControl.GetMeal(_mealName).Ingredients;
        
        foreach (KeyValuePair<Food.Ingredient, double> kvp in mealIngredients)
        {
            _app.FoodControl.EditIngredientStock(kvp.Key.Name, +kvp.Value, _user.Name);
        }

        onFinished?.Invoke();
    }
}