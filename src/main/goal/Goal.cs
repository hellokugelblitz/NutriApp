namespace NutriApp.Goal;

using System.Collections.Generic;
using Newtonsoft.Json;
using NutriApp.Workout;


public interface Goal
{
    /// <summary>
    /// The weight in pounds that the user wants to reach
    /// </summary>
    double WeightGoal { get; }

    /// <summary>
    /// The number of calories the user should consume per day
    /// </summary>
    double DailyCalorieGoal { get; }

    /// <summary>
    /// The type of goal the user has
    /// </summary>
    string Type { get; }

    /// <summary>
    /// Compares the user's weight with the goal weight. If the correct
    /// condition is met, then perform some action and return true.
    /// Otherwise, return false.
    /// </summary>
    /// <param name="userWeight">The user's current weight.</param>
    /// <returns>Whether the user reached the condition specified in the method.</returns>
    bool CheckWeight(double userWeight);

    /// <summary>
    /// Incorporates fitness into the user's goal.
    /// </summary>
    /// <param name="recommendedWorkouts"></param>
    void IncorporateFitness(List<Workout> recommendedWorkouts);

    /// <summary>
    /// Returns a dictionary of the goal's properties, for serialization.
    /// </summary>
    /// <returns></returns>
    Dictionary<string, object> ToDictionary => new()
    {
        {"weightGoal", WeightGoal},
        {"dailyCalorieGoal", DailyCalorieGoal},
        {"type", Type}
    };

}