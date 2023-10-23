using System;

namespace NutriApp.UI;

class PTViewMealsUpdater : Updater
{
    public PTViewMealsUpdater(ViewMealsCommand command, App app) : base(app)
    {
        command.Subscribe(Update);
    }
    public override void Update()
    {
        foreach (var meal in _app.FoodControl.Meals)
        {
            Console.WriteLine(meal);
        }
    }
}