using System;

namespace NutriApp.UI;

class PTPurchaseFoodUpdater : Updater
{
    public PTPurchaseFoodUpdater(PurchaseFoodCommand purchaseFoodCommand, App app) : base(app)
    {
        purchaseFoodCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine("You added the ingredient to the stock.");
    }
}