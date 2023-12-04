using NutriApp.Save;

namespace NutriApp.Workout;

using NutriApp.History;
using System.Collections.Generic;
using System.Linq;

public class WorkoutController
{
    /// <summary>
    /// Generates recommended workouts based on the list of workouts
    /// the user has done in the past.
    /// </summary>
    /// <param name="workoutEntries">The user's previous workouts</param>
    /// <returns>A short list of recommended workouts.</returns>
    public List<Workout> GenerateRecommendedWorkouts(List<Entry<Workout>> workoutEntries)
    {
        var workouts = workoutEntries
            .ConvertAll(entry => entry.Value)
            .Distinct()
            .Take(4)
            .ToList();
        return workouts;
    }
}