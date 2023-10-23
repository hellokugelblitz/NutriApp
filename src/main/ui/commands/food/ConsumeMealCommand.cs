using System;
using NutriApp.Food;

namespace NutriApp.UI;

class ConsumeMealCommand : Command<string>
{
    private App app;

    public ConsumeMealCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(string userinput)
    {
        bool valid = app.FoodControl.ConsumeMeal(userinput);
        onFinished?.Invoke();
    }
}