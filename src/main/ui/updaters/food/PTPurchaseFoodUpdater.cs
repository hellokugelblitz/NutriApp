using System;

namespace NutriApp.UI;

class PTPurchaseFoodUpdater : Updater
{
    public PTPurchaseFoodUpdater(PurchaseFoodCommand purchaseFoodCommand)
    {
        purchaseFoodCommand.Subscribe(Update);
    }

    public void Update()
    {
        Console.WriteLine("You added the ingredient to the stock.");
    }
}