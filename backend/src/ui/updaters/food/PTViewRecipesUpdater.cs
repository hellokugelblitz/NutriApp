using System;

namespace NutriApp.UI;

class PTViewRecipesUpdater : Updater
{
    public PTViewRecipesUpdater(ViewRecipesCommand command, App app) : base(app)
    {
        command.Subscribe(Update);
    }
    
    public override void Update()
    {
        foreach (var recipe in _app.FoodControl.Recipes)
        {
            Console.WriteLine(recipe);
        }
    }
}