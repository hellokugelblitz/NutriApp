using System;
using System.Collections.Generic;
using NutriApp.Food;
using NutriApp.Workout;

namespace NutriApp.History;
public class HistoryController {
    private App app;
    private List<Entry<Workout.Workout>> workouts = new();
    private List<Entry<double>> weights = new ();
    private List<Entry<Meal>> meals = new ();
    private List<Entry<(double, double)>> calories = new();
    
    public List<Entry<double>> Weights  => weights; 
    public List<Entry<Meal>> Meals => meals; 
    public List<Entry<(double, double)>> Calories => calories; 
    public List<Entry<Workout.Workout>> Workouts => workouts;
    
    public HistoryController(App app) 
    {
        this.app = app;
    }
    
//    public AddWorkout(Workout workout)
    public void SetWeight(double weight) 
    {
        weights.Add(new Entry<double>(app.TimeStamp, weight));
    }

    public void AddMeal(Meal meal) 
    {
        meals.Add(new Entry<Meal>(app.TimeStamp, meal));
    }

    public void AddCalories(DateTime date) 
    {
        calories.Add(new Entry<(double, double)>(app.TimeStamp, (GetCalorieCount(date), -1d)));
    }

    public void AddWorkout(Workout.Workout workout) 
    {
        workouts.Add(new Entry<Workout.Workout>(app.TimeStamp, workout));
    }

    public double GetCalorieCount(DateTime date) 
    {
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