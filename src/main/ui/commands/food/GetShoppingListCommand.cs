using System;
using NutriApp.Food;

namespace NutriApp.UI;

class GetShoppingListCommand : Command<string>
{
    private App app;

    public GetShoppingListCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(string userinput)
    {
        app.FoodControl.GetShoppingList();
        onFinished?.Invoke();
    }
}