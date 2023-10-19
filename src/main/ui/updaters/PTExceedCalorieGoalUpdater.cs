namespace NutriApp.UI;

using System;

public class PTExceedCalorieGoalUpdater : Updater
{
    private App app;

    public PTExceedCalorieGoalUpdater(App app) : base(app)
    {
        this.app = app;
    }

    public override void Update()
    {
        var workouts = app.GetRecommendedWorkouts();
        var msg = "You have exceeded your calorie goal for today. Consider doing the following workouts to burn some calories:\n";
        for (int i = 0; i < workouts.Count; i++)
        {
            msg += $"{i + 1}. {workouts[i].Name}\n";
        }
        Console.WriteLine(msg);
    }
}