namespace NutriApp.Workout;

public class Workout {
    public int Minutes { get; }
    public WorkoutIntensity Intensity { get; }

    public Workout(int minutes, WorkoutIntensity intensity) {
        this.Minutes = minutes;
        this.Intensity = intensity;
    }

    public int GetCaloriesBurned() {
        return (int) (Minutes * Intensity.Value());
    }
}