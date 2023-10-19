using System;
using System.Collections.Generic;
using NutriApp.Food;
using NutriApp.Workout;

namespace NutriApp.History;
public class HistoryController {
    private App app;
    private List<Entry<Workout.Workout>> workouts = new ();
    private List<Entry<double>> weights = new ();
    private List<Entry<Food.Meal>> meals = new ();
    private List<Entry<(double, double)>> calories = new();
    
    public List<Entry<Workout.Workout>> Workouts { get => workouts; }
    public List<Entry<double>> Weights { get => weights; }
    public List<Entry<Food.Meal>> Meals { get => meals; }
    public List<Entry<(double, double)>> Calories { get => calories; }
    public HistoryController(App app) {
        this.app = app;
    }
    
//    public AddWorkout(Workout workout)
    public void SetWeight(double weight) {
        weights.Add(new Entry<double>(app.TimeStamp, weight));
    }

    public void AddMeal(Food.Meal meal) {
        meals.Add(new Entry<Food.Meal>(app.TimeStamp, meal));
    }

    public void AddCalories(DateTime date) {
        
    }

    public double GetCaloreCount(DateTime date) {
        double calorieCount = 0;
        DateTime timeStamp = app.TimeStamp;
        foreach (var meal in meals) {
            if(meal.TimeStamp != timeStamp) break;

            calorieCount += meal.Value.Calories;
        }

        return calorieCount;
    }
    public void ClearHistory() { }

    private void Save() { }
    private void Load() { }

}