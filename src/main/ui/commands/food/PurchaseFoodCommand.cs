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
        int quantity = -1;
        while (quantity < 0)
        {
            try
            {
                quantity = int.Parse(userinput.GetValue(1).ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine("that is not a valid number");
            }
        }

        app.FoodControl.AddIngredientStock(name, quantity);
        onFinished?.Invoke();
    }
}