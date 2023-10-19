using System;

namespace NutriApp.UI;

class PTGetShoppingListUpdater : Updater
{
    public PTGetShoppingListUpdater(GetShoppingListCommand getShoppingListCommand, App app) : base(app)
    {
        getShoppingListCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine("You got the Shopping List.");
    }
}