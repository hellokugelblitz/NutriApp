namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public class MaintainWeightGoal : Goal {
    private readonly GoalController controller;
    public double WeightGoal { get; }
    public int DailyCalorieGoal { get; }

    public MaintainWeightGoal(GoalController controller, double weightGoal)
    {
        this.controller = controller;
        this.WeightGoal = weightGoal;
        this.DailyCalorieGoal = 2500;
    }

    public void CheckWeight(double userWeight) {}

	public void IncorporateFitness(List<Workout> recommendedWorkouts) {}
}