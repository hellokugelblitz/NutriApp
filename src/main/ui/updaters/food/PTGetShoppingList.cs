using System;

namespace NutriApp.UI;

class PTGetShoppingListUpdater : Updater
{
    public PTGetShoppingListUpdater(GetShoppingListCommand getShoppingListCommand)
    {
        getShoppingListCommand.Subscribe(Update);
    }

    public void Update()
    {
        Console.WriteLine("You got the Shopping List.");
    }
}