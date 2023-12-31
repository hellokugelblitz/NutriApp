namespace NutriApp.Goal;

using System.Collections.Generic;
using System.Linq;
using NutriApp.Workout;

public class FitnessGoal : GoalDecorator
{
    public List<Workout> RecommendedWorkouts { get; }
    public override double DailyCalorieGoal => goal.DailyCalorieGoal + GetAdditionalCalories();

    public FitnessGoal(Goal goal, List<Workout> recommendedWorkouts) : base(goal)
    {
        RecommendedWorkouts = recommendedWorkouts;
    }
    
    public override Goal CheckWeight(double userWeight)
        => new FitnessGoal(goal.CheckWeight(userWeight), RecommendedWorkouts);

    /// <summary>
    /// Gets the number of additional calories the user should consume per day
    /// in order to compensate for the recommended workouts
    /// </summary>
    /// <returns>The number of additional calories.</returns>
    public double GetAdditionalCalories() =>
        RecommendedWorkouts.Aggregate(0, (acc, w) => acc + w.GetCaloriesBurned());
    
    public override Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> data = new Dictionary<string, string>(goal.ToDictionary());
        data["isFitness"] = "true";
        return data;
    }
}