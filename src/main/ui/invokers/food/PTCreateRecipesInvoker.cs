using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTCreateRecipesInvoker : CommandInvoker<Recipe>
{
    public PTCreateRecipesInvoker(Command<Recipe> command) : base(command) { }

    public override void Invoke()
    {

    }
}