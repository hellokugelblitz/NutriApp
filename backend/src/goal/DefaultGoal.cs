using System;
using System.Collections.Generic;

namespace NutriApp.Goal;

public class DefaultGoal : Goal
{
    public double WeightGoal => throw new Exception("Set the goal");
    public double DailyCalorieGoal => throw new Exception("Set the goal");
    public Goal CheckWeight(double userWeight) => this;

    public void IncorporateFitness(List<Workout.Workout> recommendedWorkouts)
    {
        throw new Exception("Set the goal");    }

    public Dictionary<string, string> ToDictionary()
    {
        throw new Exception("Set the goal");    }
}