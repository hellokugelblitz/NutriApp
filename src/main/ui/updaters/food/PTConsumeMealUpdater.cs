using System;

namespace NutriApp.UI;

class PTConsumeMealUpdater : Updater
{
    public PTConsumeMealUpdater(ConsumeMealCommand consumeMealCommand, App app): base(app)
    {
        consumeMealCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine("Meal was consumed!");
    }
}