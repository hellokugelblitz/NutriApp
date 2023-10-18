namespace NutriApp.Workout;

using NutriApp.History;
using System.Collections.Generic;
using System.Linq;

public class WorkoutController
{
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