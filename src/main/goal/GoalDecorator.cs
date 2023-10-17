namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public abstract class GoalDecorator : Goal
{
    protected Goal goal;
    public double WeightGoal { get; }
    public int DailyCalorieGoal { get; }

    public GoalDecorator(Goal goal)
    {
        this.goal = goal;
    }

    public bool CheckWeight(double userWeight)
    {
        return goal.CheckWeight(userWeight);
    }

    public void IncorporateFitness(List<Workout> recommendedWorkouts)
    {
        goal.IncorporateFitness(recommendedWorkouts);
    }
}