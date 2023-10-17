using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PurchaseFoodCommand : Command<Food.Food>
{
    private App app;

    public PurchaseFoodCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Food.Food userinput)
    {

    }
}