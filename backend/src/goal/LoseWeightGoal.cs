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

    /// <summary>
    /// Checks if the user's weight is less than or equal to the goal weight.
    /// If true, switches the user's goal to maintaining weight.
    /// </summary>
    /// <param name="userWeight">The user's current weight.</param>
    /// <returns>Whether the goal was switched.</returns>
    public Goal CheckWeight(double userWeight)
        => userWeight <= WeightGoal ? new MaintainWeightGoal(controller, WeightGoal) : this;

    public void IncorporateFitness(List<Workout> recommendedWorkouts)
    {
        controller.Goal = new FitnessGoal(this, recommendedWorkouts);
    }
}