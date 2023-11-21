using System;

namespace NutriApp.UI;

class PTViewTargetCaloriesUpdater : Updater
{
    public PTViewTargetCaloriesUpdater(ViewTargetCaloriesCommand viewTargetCaloriesCommand, App app): base(app)
    {
        viewTargetCaloriesCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine($"You should consume {_app.GoalControl.Goal.DailyCalorieGoal} calories today");
    }
}