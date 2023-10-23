using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PurchaseFoodCommand : Command<(string, double)>
{
    private App app;

    public PurchaseFoodCommand(App app)
    {
        this.app = app;
    }

    public override void Execute((string, double) userinput)
    {
        string name = userinput.Item1;
        double quantity = userinput.Item2;

        app.FoodControl.AddIngredientStock(name, quantity);
        onFinished?.Invoke();
    }
}