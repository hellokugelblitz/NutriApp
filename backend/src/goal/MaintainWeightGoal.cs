namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;


public class MaintainWeightGoal : Goal
{
    private readonly GoalController controller;
    public double WeightGoal { get; }
    public double DailyCalorieGoal { get; }

    public MaintainWeightGoal(GoalController controller, double weightGoal)
    {
        this.controller = controller;
        this.WeightGoal = weightGoal;
        this.DailyCalorieGoal = 2500;
    }

    /// <summary>
    /// Checks if the user's weight is within 5 pounds of the goal weight.
    /// If not, switches the user's goal to either losing or gaining weight.
    /// </summary>
    /// <param name="userWeight">The user's current weight.</param>
    /// <returns>Whether the goal was switched.</returns>
    public Goal CheckWeight(double userWeight)
    {
        const int threshold = 5;
        return userWeight <= WeightGoal - threshold ?
            new LoseWeightGoal(controller, WeightGoal) :
            userWeight >= WeightGoal + threshold ?
                new GainWeightGoal(controller, WeightGoal) :
                this;
    }

    public void IncorporateFitness(List<Workout> recommendedWorkouts)
    {
        controller.Goal = new FitnessGoal(this, recommendedWorkouts);
    }
}