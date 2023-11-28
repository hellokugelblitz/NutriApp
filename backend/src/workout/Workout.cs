using System;
using System.Collections.Generic;
using NutriApp.History;
using NutriApp.Save;

namespace NutriApp.Workout;

public class Workout : IHistorySaveable {
    public string Name { get; private set; }
    public int Minutes { get; private set; }
    public WorkoutIntensity Intensity { get; private set; }

    public Workout(string name, int minutes, WorkoutIntensity intensity) {
        Name = name;
        Minutes = minutes;
        Intensity = intensity;
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

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> data = new();

        data["Name"] = Name;
        data["Minutes"] = Minutes.ToString();
        data["Intensity"] = Intensity.ToString();

        return data;
    }

    public void FromDictionary(Dictionary<string, string> data)
    {
        Name = data["Name"];
        Minutes = Int32.Parse(data["Minutes"]);
        Intensity = Enum.Parse<WorkoutIntensity>(data["Intensity"]);
    }

    public string ToSaveString()
    {
        return Name + IHistorySaveable.HISTORY_SAVEABLE_SEPERATOR + Minutes + IHistorySaveable.HISTORY_SAVEABLE_SEPERATOR + Intensity;
    }

    public void FromSaveString(string str)
    {
        var strs = str.Split(IHistorySaveable.HISTORY_SAVEABLE_SEPERATOR);
        Name = strs[0];
        Minutes = Int32.Parse(strs[1]);
        Intensity = Enum.Parse<WorkoutIntensity>(strs[2]);
    }
}