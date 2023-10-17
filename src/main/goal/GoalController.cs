namespace NutriApp.Goal;

public class GoalController {
    private readonly App app;
    private IGoal goal;

    public IGoal Goal { get; set; }
    public int GoalIdx { get; set; }

    public GoalController(App app) {
        this.app = app;
    }

    public void IncorporateFitness() {}
    public void CompareUserWeightToGoal() {}
    public void CompareTodaysCaloriesToGoal() {}

    private void Save() {}
    private void Load() {}
}