namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public class LoseWeightGoal : IGoal {
    private readonly GoalController controller;
    public double WeightGoal { get; }
    public int DailyCalorieGoal { get; }

    public LoseWeightGoal(GoalController controller, double weightGoal)
    {
        this.controller = controller;
        this.WeightGoal = weightGoal;
        this.DailyCalorieGoal = 500;
    }

    public void CheckWeight(double userWeight) {}

	public void IncorporateFitness(List<Workout> recommendedWorkouts) {}
}