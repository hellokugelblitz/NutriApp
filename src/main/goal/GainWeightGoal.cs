namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public class GainWeightGoal : Goal
{
    private readonly GoalController controller;
    public double WeightGoal { get; }
    public int DailyCalorieGoal { get; }

    public GainWeightGoal(GoalController controller, double weightGoal)
    {
        this.controller = controller;
        this.WeightGoal = weightGoal;
        this.DailyCalorieGoal = 3500;
    }

    public bool CheckWeight(double userWeight) { 
        if (userWeight >= WeightGoal)
        {
            controller.Goal = new MaintainWeightGoal(controller, WeightGoal);
            return true;
        }
        return false;
    }

    public void IncorporateFitness(List<Workout> recommendedWorkouts) { }
}