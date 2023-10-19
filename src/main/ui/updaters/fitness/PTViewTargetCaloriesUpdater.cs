using System;

namespace NutriApp.UI;

class PTViewTargetCaloriesUpdater : Updater
{
    public PTViewTargetCaloriesUpdater(AddWorkoutCommand addWorkoutCommand, App app): base(app)
    {
        addWorkoutCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine($"You should consume {_app.GoalControl.Goal.DailyCalorieGoal} calories today");
    }
}