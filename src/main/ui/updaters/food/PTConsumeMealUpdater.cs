using System;

namespace NutriApp.UI;

class PTConsumeMealUpdater : Updater
{
    public PTConsumeMealUpdater(ConsumeMealCommand consumeMealCommand)
    {
        consumeMealCommand.Subscribe(Update);
    }

    public void Update()
    {
        Console.WriteLine("Meal was consumed!");
    }
}