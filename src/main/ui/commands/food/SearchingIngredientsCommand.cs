using System;
using NutriApp.Food;

namespace NutriApp.UI;

class SearchingIngredientsCommand : Command<Ingredient>
{
    private App app;

    public SearchingIngredientsCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Ingredient userinput)
    {
        app.FoodControl.IngredientDatabase.Search(userinput);
        onFinished?.Invoke();
    }
}