using System;
using NutriApp.Food;

namespace NutriApp.Undo;

class UndoCreateMeal : UndoCommand
{
    private FoodController _foodController;
    private Meal _meal;

    public UndoCreateMeal(FoodController foodController, Meal meal) 
    {
        _foodController = foodController;
        _meal = meal;
    }

    public override void Execute()
    {
        _foodController.RemoveMeal(_meal);

        onFinished?.Invoke();
    }
}