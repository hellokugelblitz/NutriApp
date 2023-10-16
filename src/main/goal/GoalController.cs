namespace NutriApp.Goal;

using NutriApp.Workout;

public class GoalController
{
    private readonly App app;
    private IGoal goal;

    public IGoal Goal { get; set; }
    public int GoalIdx { get; set; }

    public GoalController(App app)
    {
        this.app = app;
    }

    public void IncorporateFitness(List<Workout> recommendedWorkouts) { }
    public void CompareUserWeightToGoal() { }
    public void CompareTodaysCaloriesToGoal() { }

    private void Save() { }
    private void Load() { }
}