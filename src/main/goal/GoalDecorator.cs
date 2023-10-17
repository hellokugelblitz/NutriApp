namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public abstract class GoalDecorator : IGoal
{
    protected IGoal goal;
    public double WeightGoal { get; }
    public int DailyCalorieGoal { get; }

    public GoalDecorator(IGoal goal)
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