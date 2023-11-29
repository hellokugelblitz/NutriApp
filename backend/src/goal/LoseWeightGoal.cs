namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;


public class LoseWeightGoal : Goal
{

    private readonly GoalController controller;
    public double WeightGoal { get; private set; }
    public double DailyCalorieGoal { get; }

    private string username;

    public LoseWeightGoal(GoalController controller, double weightGoal, string username)
    {
        this.controller = controller;
        this.WeightGoal = weightGoal;
        this.DailyCalorieGoal = 1500;
        this.username = username;
    }

    /// <summary>
    /// Checks if the user's weight is less than or equal to the goal weight.
    /// If true, switches the user's goal to maintaining weight.
    /// </summary>
    /// <param name="userWeight">The user's current weight.</param>
    /// <returns>Whether the goal was switched.</returns>
    public Goal CheckWeight(double userWeight)
        => userWeight <= WeightGoal ? new MaintainWeightGoal(controller, WeightGoal, username) : this;

    public void IncorporateFitness(List<Workout> recommendedWorkouts)
    {
        controller.SetGoal(new FitnessGoal(this, recommendedWorkouts), username);
    }
    
    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> data = new();
        data["WeightGoal"] = WeightGoal.ToString();
        return data;
    }
}