namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public class FitnessGoal : GoalDecorator
{
    public List<Workout> RecommendedWorkouts { get; }

    public FitnessGoal(Goal goal, List<Workout> recommendedWorkouts) : base(goal)
    {
        this.RecommendedWorkouts = recommendedWorkouts;
    }

    public double GetAdditionalCalories()
    {
        return -1;
    }
}