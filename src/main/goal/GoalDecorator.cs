namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public abstract class GoalDecorator : Goal
{
    protected Goal goal;

    public double WeightGoal => goal.WeightGoal;
    public double DailyCalorieGoal => goal.DailyCalorieGoal;

    public GoalDecorator(Goal goal)
    {
        this.goal = goal;
    }

    public virtual Goal CheckWeight(double userWeight) => goal.CheckWeight(userWeight);
    public void IncorporateFitness(List<Workout> recommendedWorkouts) => goal.IncorporateFitness(recommendedWorkouts);
}