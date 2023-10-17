namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public class LoseWeightGoal : Goal
{
    private readonly GoalController controller;
    public double WeightGoal { get; }
    public double DailyCalorieGoal { get; }

    public LoseWeightGoal(GoalController controller, double weightGoal)
    {
        this.controller = controller;
        this.WeightGoal = weightGoal;
        this.DailyCalorieGoal = 1500;
    }

    public bool CheckWeight(double userWeight) { 
        if (userWeight <= WeightGoal)
        {
            controller.Goal = new MaintainWeightGoal(controller, WeightGoal);
            return true;
        }
        return false;
    }

    public void IncorporateFitness(List<Workout> recommendedWorkouts) { }
}