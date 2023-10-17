using System;
using NutriApp.Food;

namespace NutriApp;

class CreateRecipeCommand : Command<Recipe>
{
    private App app;

    public CreateRecipeCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Recipe userinput)
    {
        
    }
}