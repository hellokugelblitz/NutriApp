using System;
using System.Collections.Generic;
using NutriApp.History;
using NutriApp.Save;

namespace NutriApp.Workout;

public class Workout {
    public string Name { get; private set; }
    public int Minutes { get; private set; }
    public WorkoutIntensity Intensity { get; private set; }

    public Workout(string name, int minutes, WorkoutIntensity intensity) {
        Name = name;
        Minutes = minutes;
        Intensity = intensity;
    }
    
    public Workout(string name, int minutes, double intensity) {
        Name = name;
        Minutes = minutes;
        switch (intensity)
        {
            case 5:
                Intensity = WorkoutIntensity.LOW;
                break;
            case 10:
                Intensity = WorkoutIntensity.MEDIUM;
                break;
            case 15:
                Intensity = WorkoutIntensity.HIGH;
                break;
        }
    }

    public Workout() { }

    /// <summary>
    /// Bases equality on just the name of the workouts and intensity
    /// </summary>
    /// <param name="obj">The obj to test equality for</param>
    /// <returns>Whether the two objects are equal</returns>
    public override bool Equals(object obj)
    {
        return obj is Workout workout && workout.Name == Name && workout.Intensity == Intensity;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() + Intensity.GetHashCode();
    }

    public override string ToString()
    {
        return $"name:{Name} minutes:{Minutes} intensity:{Intensity}";
    }

    /// <returns>Minutes * Intensity</returns>
    public int GetCaloriesBurned() {
        return (int) (Minutes * Intensity.Value());
    }
}