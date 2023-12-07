using NutriApp.Workout;

namespace NutriApp.Controllers.Models;

public struct WorkoutModel
{
    public string Name { get; set; }
    public int Minutes { get; set; }
    public double Intensity { get; set; }

    public WorkoutModel(Workout.Workout workout)
    {
        Name = workout.Name;
        Minutes = workout.Minutes;
        Intensity = workout.Intensity.Value();
    }
    
    public Workout.Workout ToWorkout() => new Workout.Workout(Name, Minutes, Intensity);
}