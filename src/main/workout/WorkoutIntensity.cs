namespace NutriApp.Workout;

public enum WorkoutIntensity {
    LOW,
    MEDIUM,
    HIGH
}

public static class WorkoutIntensityExtensions {
    public static double Value(this WorkoutIntensity intensity) {
        return intensity switch {
            WorkoutIntensity.LOW => 5d,
            WorkoutIntensity.MEDIUM => 10d,
            WorkoutIntensity.HIGH => 15d,
            _ => -1d 
        };
    }
}