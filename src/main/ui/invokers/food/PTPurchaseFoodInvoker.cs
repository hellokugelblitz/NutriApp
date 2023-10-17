using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTPurchaseFoodInvoker : CommandInvoker<Food.Food>
{
    public PTPurchaseFoodInvoker(Command<Food.Food> command) : base(command) { }

    public override void Invoke()
    {

    }
}