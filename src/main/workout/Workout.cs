namespace NutriApp.Workout;

public class Workout {
    public string Name { get; set; }
    public int Minutes { get; }
    public WorkoutIntensity Intensity { get; }

    public Workout(string name, int minutes, WorkoutIntensity intensity) {
        Name = name;
        Minutes = minutes;
        Intensity = intensity;
    }

    public int GetCaloriesBurned() {
        return (int) (Minutes * Intensity.Value());
    }
}