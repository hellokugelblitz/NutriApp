namespace NutriApp.Goal;

using System.Collections.Generic;
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
}