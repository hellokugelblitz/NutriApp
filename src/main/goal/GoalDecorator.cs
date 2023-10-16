namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public abstract class GoalDecorator : IGoal
{
    private IGoal goal;
    public double WeightGoal { get; }
    public int DailyCalorieGoal { get; }

    public GoalDecorator(IGoal goal)
    {
        this.goal = goal;
    }

    public void CheckWeight(double userWeight)
    {
        goal.CheckWeight(userWeight);
    }

    public void IncorporateFitness(List<Workout> recommendedWorkouts)
    {
        goal.IncorporateFitness(recommendedWorkouts);
    }
}