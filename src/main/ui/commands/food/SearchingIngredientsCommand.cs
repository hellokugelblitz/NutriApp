using System;
using NutriApp.Food;

namespace NutriApp.UI;

class SearchingIngredientsCommand : Command<string>
{
    private App app;

    public SearchingIngredientsCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(string userinput)
    {
        app.FoodControl.SearchIngredients(userinput);
        onFinished?.Invoke();
    }
}