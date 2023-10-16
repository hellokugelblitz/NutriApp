namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public class FitnessGoal : GoalDecorator {
    public List<Workout> RecommendedWorkouts { get; }

    public FitnessGoal(IGoal goal, List<Workout> recommendedWorkouts) : base(goal) {
        this.RecommendedWorkouts = recommendedWorkouts;
    }

    public int GetAdditionalCalories() {
        return -1;
    }
}