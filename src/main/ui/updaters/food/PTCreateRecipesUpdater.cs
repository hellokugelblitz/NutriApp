using System;

namespace NutriApp.UI;

class PTCreateRecipesUpdater : Updater
{
    public PTCreateRecipesUpdater(CreateRecipesCommand createRecipesCommand, App app) : base(app)
    {
        createRecipesCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine("Recipe created successfully!");
    }
}