namespace NutriApp.UI;

using System;

public class PTExceedCalorieGoalUpdater : Updater
{
    public PTExceedCalorieGoalUpdater(App app) : base(app)
    {
        app.GoalControl.PassedCalorieGoalEvent += Update;
    }

    public override void Update()
    {
        var workouts = _app.GetRecommendedWorkouts();
        var msg = "You have exceeded your calorie goal for today. Consider doing the following workouts to burn some calories:\n";
        for (int i = 0; i < workouts.Count; i++)
        {
            msg += $"{i + 1}. {workouts[i].Name}\n";
        }
        Console.WriteLine(msg);
    }
}