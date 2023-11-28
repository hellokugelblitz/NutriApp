using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using NutriApp.Food;
using NutriApp.Save;
using NutriApp.Workout;

namespace NutriApp.History;

public class HistoryController : ISaveableController
{
    private App app;
    private Dictionary<string, HistoryObject> history = new();

    private readonly string historyPath = $"{Persistence.HistoryDataPath}history.json";

    public List<Entry<Workout.Workout>> GetWorkouts(string username) => history[username].workouts;
    public List<Entry<Food.Meal>> GetMeals(string username) => history[username].meals;
    public List<Entry<CalorieTracker>> GetCalories(string username) => history[username].calories;
    public double CurrentWeight(string username) => history[username].weights[^1].Value;

    public List<Entry<Weight>> Weights(string username) => history[username].weights;


    public HistoryController(App app)
    {
        this.app = app;
    }

    public void AddWorkout(Workout.Workout workout, string username)
    {
        history[username].workouts.Add(new Entry<Workout.Workout>(app.TimeStamp, workout));
    }

    public void SetWeight(double weight, string username)
    {
        history[username].weights.Add(new Entry<Weight>(app.TimeStamp, weight));
    }

    public void AddMeal(Food.Meal meal, string username)
    {
        history[username].meals.Add(new Entry<Food.Meal>(app.TimeStamp, meal));
    }

    public void AddCalories(DateTime date, string username)
    {
        history[username].calories.Add(new Entry<CalorieTracker>(app.TimeStamp,
            new CalorieTracker(GetCalorieCount(date), app.GoalControl.Goal.DailyCalorieGoal)));
    }

    /// <summary>
    /// calculates the calories eaten that day
    /// </summary>
    /// <param name="date">the date you are checking</param>
    /// <returns>number of calories eaten</returns>
    public double GetCalorieCount(string username)
    {
        double calorieCount = 0;
        DateTime timeStamp = app.TimeStamp;
        foreach (var meal in history[username].meals)
        {
            if (meal.TimeStamp != timeStamp) break;

            calorieCount += meal.Value.Calories;
        }

        return calorieCount;
    }

    /// <summary>
    /// saves each of the histories
    /// </summary>
    // public void Save()
    // {
    //     var serialize = new HistorySerialize(this);
    //     string json =  JsonConvert.SerializeObject(serialize);
    //     File.WriteAllText(historyPath, json);
    // }

    /// <summary>
    /// loads each of the histories in the constructor
    /// </summary>
    // public void Load()
    // {
    //     if(!File.Exists(historyPath)) return;
    //     
    //     string json = File.ReadAllText(historyPath);
    //     HistorySerialize serialize = JsonConvert.DeserializeObject<HistorySerialize>(json);
    //     serialize.Deserialize(this);
    // }

    public class HistoryObject : ISaveObject
    {
        public List<Entry<Workout.Workout>> workouts = new();
        public List<Entry<Weight>> weights = new();
        public List<Entry<Food.Meal>> meals = new();
        public List<Entry<CalorieTracker>> calories = new();

        public Dictionary<string, string> ToDictionary()
        {
            throw new NotImplementedException();
        }

        public void FromDictionary(Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }
    }

    // private class HistorySerialize : ISaveObject
    // {
    //     [JsonProperty] public List<Entry<Workout.Workout>> workouts;
    //     [JsonProperty] public List<Entry<Weight>> weights;
    //     [JsonProperty] public List<Entry<MealName>> meals;
    //     [JsonProperty] public List<Entry<CalorieTracker>> calories;
    //
    //     public HistorySerialize(HistoryController history)
    //     {
    //         workouts = history.Workouts;
    //         weights = history.Weights;
    //         meals = new();
    //         history.Meals.ForEach((meal) => { meals.Add(new Entry<MealName>(meal.TimeStamp, meal.Value.Name)); });
    //         calories = history.Calories;
    //     }
    //
    //     public HistorySerialize() { }
    //
    //     public void Deserialize(HistoryController history)
    //     {
    //         history.workouts = workouts;
    //         history.weights = weights;
    //         history.meals = new();
    //         meals.ForEach((meal) => {history.meals.Add(new Entry<Meal>(meal.TimeStamp, new Meal(meal.Value)));});
    //         history.calories = calories;
    //     }
    //
    //     public Dictionary<string, string> ToDictionary()
    //     {
    //         throw new NotImplementedException();
    //     }
    //
    //     public void FromDictionary(Dictionary<string, string> data)
    //     {
    //         throw new NotImplementedException();
    //     }
    // }

    public void SaveUser(string folderName)
    {
        throw new NotImplementedException();
    }

    public void LoadUser(string folderName)
    {
        throw new NotImplementedException();
    }

    public void SaveController()
    {
        throw new NotImplementedException();
    }

    public void LoadController()
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// data class used for the calories history
    /// </summary>
    /// <param name="ActualCalories">number of calories the user ate that day</param>
    /// <param name="TargetCalories">number of calories the user was recommended to eat that day</param>
    public class CalorieTracker : IHistorySaveable
    {
        public double ActualCalories { get; set; }
        public double TargetCalories { get; set; }

        public CalorieTracker(double actualCalories, double targetCalories)
        {
            this.ActualCalories = actualCalories;
            this.TargetCalories = targetCalories;
        }

        public CalorieTracker() { }

        public string ToSaveString()
        {
            return ActualCalories + IHistorySaveable.HISTORY_SAVEABLE_SEPERATOR + TargetCalories;
        }

        public void FromSaveString(string str)
        {
            var strs = str.Split(IHistorySaveable.HISTORY_SAVEABLE_SEPERATOR);
            ActualCalories = Int32.Parse(strs[0]);
            TargetCalories = Int32.Parse(strs[1]);
        }
    }

    public class Weight : IHistorySaveable
    {
        private double weight;

        public Weight(double d)
        {
            weight = d;
        }

        public Weight() { }


        public string ToSaveString()
        {
            return weight.ToString();
        }

        public void FromSaveString(string str)
        {
            weight = double.Parse(str);
        }

        public static implicit operator double(Weight w) => w.weight;
        public static implicit operator Weight(double d) => new Weight(d);

    }

    public class MealName : IHistorySaveable
    {
        private string name;
        private static FoodController _foodController;

        public MealName(string s) : this()
        {
            name = s;
        }

        public MealName()
        {
            _foodController = app.FoodControl;
        }


        public string ToSaveString()
        {
            return name;
        }

        public void FromSaveString(string str)
        {
            name = str;
        }

        public static implicit operator string(MealName m) => m.name;
        public static implicit operator MealName(string s) => new MealName(s);
        public static implicit operator Meal(MealName m) => .GetMeal(m.name);

    }

}
