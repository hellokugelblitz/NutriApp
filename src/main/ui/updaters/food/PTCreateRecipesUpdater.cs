using System;

namespace NutriApp.UI;

class PTCreateRecipesUpdater : Updater
{
    public PTCreateRecipesUpdater(CreateRecipesCommand createRecipesCommand)
    {
        createRecipesCommand.Subscribe(Update);
    }

    public void Update()
    {
        Console.WriteLine("Recipe created successfully!");
    }
}