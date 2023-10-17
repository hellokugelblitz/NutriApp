namespace NutriApp.Goal;

using NutriApp.Workout;

public class GoalController
{
    private readonly App app;
    private IGoal goal;

    public IGoal Goal { get; set; }
    public void SetGoalByIndex(int index, int weight) {
        switch (index)
        {
            case 0:
                Goal = new LoseWeightGoal(this, weight);
                break;
            case 1:
                Goal = new MaintainWeightGoal(this, weight);
                break;
            case 2:
                Goal = new GainWeightGoal(this, weight);
                break;
            default:
                throw new Exception("Invalid goal index");
        }
    }

    public GoalController(App app)
    {
        this.app = app;
    }

    public void IncorporateFitness(List<Workout> recommendedWorkouts) { 
        var workouts = app.GetRecommendedWorkouts();
        goal.IncorporateFitness(workouts);
    }
    public void CompareUserWeightToGoal() { 
        goal.CheckWeight(app.User.GetWeight);
    }
    public void CompareTodaysCaloriesToGoal() { }

    private void Save() { }
    private void Load() { }
}