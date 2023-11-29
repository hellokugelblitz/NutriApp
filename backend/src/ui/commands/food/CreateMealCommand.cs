using System;
using NutriApp.Food;

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

    public override void Execute(Meal userinput)
    {
        _app.FoodControl.AddMeal(userinput);

        

        onFinished?.Invoke();
    }
}