using System;
using NutriApp.Food;

namespace NutriApp;

class CreateRecipesCommand : Command<Recipe>
{
    private App app;

    public CreateRecipesCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Recipe userinput)
    {
        app.FoodControl.AddRecipe(userinput);
        onFinished?.Invoke();
    }
}