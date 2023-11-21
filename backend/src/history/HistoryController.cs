using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using NutriApp.Food;
using NutriApp.Workout;

namespace NutriApp.History;
public class HistoryController {
    private App app;

    private List<Entry<Workout.Workout>> workouts = new();
    private List<Entry<double>> weights = new ();
    private List<Entry<Food.Meal>> meals = new ();
    private List<Entry<CalorieTracker>> calories = new();
    private readonly string historyPath = $"{Persistence.HistoryDataPath}history.json";
    
    public List<Entry<Workout.Workout>> Workouts => workouts;
    public List<Entry<Food.Meal>> Meals => meals; 
    public List<Entry<CalorieTracker>> Calories => calories;
    public double CurrentWeight => weights[^1].Value; 

    public List<Entry<double>> Weights
    {
        get => weights;
        set => weights = value;
    }


    public HistoryController(App app) {
        this.app = app;
        Load();
    }

    public void AddWorkout(Workout.Workout workout)
    {
        workouts.Add(new Entry<Workout.Workout>(app.TimeStamp, workout));
    }
    
    public void SetWeight(double weight) {
        weights.Add(new Entry<double>(app.TimeStamp, weight));
    }

    public void AddMeal(Food.Meal meal) {
        meals.Add(new Entry<Food.Meal>(app.TimeStamp, meal));
    }

    public void AddCalories(DateTime date) {
        calories.Add(new Entry<CalorieTracker>(app.TimeStamp, new CalorieTracker(GetCalorieCount(date), app.GoalControl.Goal.DailyCalorieGoal)));
    }

    /// <summary>
    /// calculates the calories eaten that day
    /// </summary>
    /// <param name="date">the date you are checking</param>
    /// <returns>number of calories eaten</returns>
    public double GetCalorieCount(DateTime date) {
        double calorieCount = 0;
        DateTime timeStamp = app.TimeStamp;
        foreach (var meal in meals) {
            if(meal.TimeStamp != timeStamp) break;

            calorieCount += meal.Value.Calories;
        }

        return calorieCount;
    }
    
    /// <summary>
    /// clears the history of everything. This is used for developer purposes
    /// </summary>
    public void ClearHistory() { }

    /// <summary>
    /// saves each of the histories
    /// </summary>
    public void Save()
    {
        var serialize = new HistorySerialize(this);
        string json =  JsonConvert.SerializeObject(serialize);
        File.WriteAllText(historyPath, json);
    }

    /// <summary>
    /// loads each of the histories in the constructor
    /// </summary>
    public void Load()
    {
        if(!File.Exists(historyPath)) return;
        
        string json = File.ReadAllText(historyPath);
        HistorySerialize serialize = JsonConvert.DeserializeObject<HistorySerialize>(json);
        serialize.Deserialize(this);
    }

    [Serializable]
    private class HistorySerialize
    {
        [JsonProperty] public List<Entry<Workout.Workout>> workouts;
        [JsonProperty] public List<Entry<double>> weights;
        [JsonProperty] public List<Entry<string>> meals;
        [JsonProperty] public List<Entry<CalorieTracker>> calories;

        public HistorySerialize(HistoryController history)
        {
            workouts = history.Workouts;
            weights = history.Weights;
            meals = new();
            history.Meals.ForEach((meal) => { meals.Add(new Entry<string>(meal.TimeStamp, meal.Value.Name)); });
            calories = history.Calories;
        }

        public HistorySerialize() { }

        public void Deserialize(HistoryController history)
        {
            history.workouts = workouts;
            history.weights = weights;
            history.meals = new();
            meals.ForEach((meal) => {history.meals.Add(new Entry<Meal>(meal.TimeStamp, new Meal(meal.Value)));});
            history.calories = calories;
        }
    }
}

/// <summary>
/// data class used for the calories history
/// </summary>
/// <param name="ActualCalories">number of calories the user ate that day</param>
/// <param name="TargetCalories">number of calories the user was recommended to eat that day</param>
public record CalorieTracker(double ActualCalories, double TargetCalories);
