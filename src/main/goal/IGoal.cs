namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public interface IGoal {
    double WeightGoal { get; }
    int DailyCalorieGoal { get; }
    void CheckWeight(double userWeight);
	void IncorporateFitness(List<Workout> recommendedWorkouts);
}