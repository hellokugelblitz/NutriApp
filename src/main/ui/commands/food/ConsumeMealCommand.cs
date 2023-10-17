using System;
using NutriApp.Food;

namespace NutriApp.UI;

class ConsumeMealCommand : Command<Meal>
{
    private App app;

    public ConsumeMealCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Meal userinput)
    {
        
    }
}