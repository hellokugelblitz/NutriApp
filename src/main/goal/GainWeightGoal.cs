namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public class GainWeightGoal : Goal
{

    private readonly GoalController controller;
    public double WeightGoal { get; }
    public double DailyCalorieGoal { get; }

    public GainWeightGoal(GoalController controller, double weightGoal)
    {
        this.controller = controller;
        this.WeightGoal = weightGoal;
        this.DailyCalorieGoal = 3500;
    }

    /// <summary>
    /// Checks if the user's weight is greater than or equal to the goal weight.
    /// If true, switches the user's goal to maintaining weight.
    /// </summary>
    /// <param name="userWeight">The user's current weight.</param>
    /// <returns>Whether the goal was switched.</returns>
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