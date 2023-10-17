namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public class GainWeightGoal : Goal {
    private readonly GoalController controller;
    public double WeightGoal { get; }
    public int DailyCalorieGoal { get; }

    public GainWeightGoal(GoalController controller, double weightGoal)
    {
        this.controller = controller;
        this.WeightGoal = weightGoal;
        this.DailyCalorieGoal = 3500;
    }

    public void CheckWeight(double userWeight) {}

	public void IncorporateFitness(List<Workout> recommendedWorkouts) {}
}