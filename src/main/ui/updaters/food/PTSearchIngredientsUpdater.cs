using System;

namespace NutriApp.UI;

class PTSearchIngredientsUpdater : Updater
{
    public PTSearchIngredientsUpdater(SearchingIngredientsCommand searchingIngredientsCommand, App app): base(app)
    {
        searchingIngredientsCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine("Loading up ingredient...");
    }
}