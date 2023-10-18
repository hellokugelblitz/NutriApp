using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PurchaseFoodCommand : Command<Array>
{
    private App app;

    public PurchaseFoodCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Array userinput)
    {
        string name = userinput.GetValue(0).ToString();
        int quantity = int.Parse(userinput.GetValue(1).ToString());

        app.FoodControl.AddIngredientStock(name, quantity);
        onFinished?.Invoke();
    }
}