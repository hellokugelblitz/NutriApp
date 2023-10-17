namespace NutriApp;

public class PTExceedCalorieGoalUpdater : Updater
{
    private App app;

    public PTExceedCalorieGoalUpdater(App app)
    {
        this.app = app;
    }

    public void Update()
    {
        var workouts = app.GetRecommendedWorkouts();
    }
}