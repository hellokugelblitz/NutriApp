using System;

namespace NutriApp.UI;

class PTSearchIngredientsUpdater : Updater
{
    public PTSearchIngredientsUpdater(SearchingIngredientsCommand searchingIngredientsCommand)
    {
        searchingIngredientsCommand.Subscribe(Update);
    }

    public void Update()
    {
        Console.WriteLine("Loading up ingredient...");
    }
}