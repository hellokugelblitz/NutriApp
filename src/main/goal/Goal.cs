namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public interface Goal
{
    double WeightGoal { get; }
    double DailyCalorieGoal { get; }
    bool CheckWeight(double userWeight);
    void IncorporateFitness(List<Workout> recommendedWorkouts);
}