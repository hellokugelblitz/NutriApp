using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTConsumeMealInvoker : CommandInvoker<Meal>
{
    public PTConsumeMealInvoker(Command<Meal> command) : base(command) { }

    public override void Invoke()
    {

    }
}