using System;
using System.Collections.Generic;
using NutriApp.Food;
using NutriApp.Save;
using NutriApp.Workout;

namespace NutriApp.History;

public class HistoryController : ISaveableController
{
    private App _app;
    private ISaveSystem _saveSystem;
    private Dictionary<string, HistoryObject> history = new(); //username to history data

    private const string EntryValueSep = ","; //seperator between values of the entrie and generic(datetime,weight)
    private const string EntrySep = " | "; //seperator between entries

    public List<Entry<Workout.Workout>> GetWorkouts(string username) => history[username].Workouts;
    public List<Entry<Meal>> GetMeals(string username) => history[username].Meals;
    public List<Entry<CalorieTracker>> GetCalories(string username) => history[username].Calories;
    public double CurrentWeight(string username) => history[username].Weights[^1].Value;

    public List<Entry<double>> Weights(string username) => history[username].Weights;


    public HistoryController(App app, ISaveSystem saveSystem)
    {
        _app = app;
        _saveSystem = saveSystem;
    }

    public void AddWorkout(Workout.Workout workout, string username)
    {
        history[username].Workouts.Add(new Entry<Workout.Workout>(_app.TimeStamp, workout));
    }

    public void SetWeight(double weight, string username)
    {
        history[username].Weights.Add(new Entry<double>(_app.TimeStamp, weight));
    }

    public void AddMeal(Meal meal, string username)
    {
        history[username].Meals.Add(new Entry<Meal>(_app.TimeStamp, meal));
    }

    public void AddCalories(string username)
    {
        history[username].Calories.Add(new Entry<CalorieTracker>(_app.TimeStamp,
            new CalorieTracker(GetCalorieCount(username), _app.GoalControl.GetGoal(username).DailyCalorieGoal)));
    }

    /// <summary>
    /// calculates the calories eaten that day
    /// </summary>
    /// <param name="username">the username your are checking for</param>
    /// <returns>number of calories eaten on the current day</returns>
    public double GetCalorieCount(string username)
    {
        double calorieCount = 0;
        DateTime timeStamp = _app.TimeStamp;
        foreach (var meal in history[username].Meals)
        {
            if (meal.TimeStamp != timeStamp) break;

            calorieCount += meal.Value.Calories;
        }

        return calorieCount;
    }

    public void SaveUser(string folderName)
    {
        string username = SaveSystem.GetUsernameFromFile(folderName);
        _saveSystem.GetFileSaver().Save(SaveSystem.GetFullPath(folderName,"history"), history[username].ToDictionary());
        history.Remove(username);
    }

    public void LoadUser(string folderName)
    {
        string username = SaveSystem.GetUsernameFromFile(folderName);
        Dictionary<string, string> data = _saveSystem.GetFileSaver().Load(SaveSystem.GetFullPath(folderName, "history"));
        HistoryObject obj = new HistoryObject(_app.FoodControl);
        obj.FromDictionary(data);
        history[username] = obj;
    }

    /// <summary>
    /// satisfy ISaveableController requirments
    /// </summary>
    public void SaveController() { }

    /// <summary>
    /// satisfy ISaveableController requirments
    /// </summary>
    public void LoadController() { }

    public void AddNewUser(User user)
    {
        if(history.ContainsKey(user.UserName)) return;
        history[user.UserName] = new HistoryObject(_app.FoodControl);
    }


    public class HistoryObject : ISaveObject
    {
        public readonly List<Entry<Workout.Workout>> Workouts = new();
        public readonly List<Entry<double>> Weights = new();
        public readonly List<Entry<Meal>> Meals = new();
        public readonly List<Entry<CalorieTracker>> Calories = new();

        private readonly FoodController _foodController;

        public HistoryObject(FoodController foodController)
        {
            _foodController = foodController;
        }
        
        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> data = new();
            string workoutString = "";
            foreach (var workout in Workouts)
            {
                workoutString += workout.TimeStamp + EntryValueSep + workout.Value.Name + EntryValueSep +
                                 workout.Value.Minutes + EntryValueSep + workout.Value.Intensity + EntrySep;
            }

            if(workoutString.Length > 0) workoutString = workoutString.Substring(0, workoutString.Length - EntrySep.Length);
            data["workouts"] = workoutString;

            string weightString = "";
            foreach (var weight in Weights)
            {
                weightString += weight.TimeStamp + EntryValueSep + weight.Value + EntrySep;
            }

            if(weightString.Length > 0) weightString = weightString.Substring(0, weightString.Length - EntrySep.Length);
            data["weights"] = weightString;
            
            string mealString = "";
            foreach (var meal in Meals)
            {
                mealString += meal.TimeStamp + EntryValueSep + meal.Value.Name + EntrySep;
            }

            if(mealString.Length > 0)mealString = mealString.Substring(0, mealString.Length - EntrySep.Length);
            data["meals"] = mealString;
            
            string calorieString = "";
            foreach (var calorie in Calories)
            {
                calorieString += calorie.TimeStamp + EntryValueSep + calorie.Value.ActualCalories + EntryValueSep +
                                 calorie.Value.TargetCalories + EntrySep;
            }

            if(calorieString.Length > 0) calorieString = calorieString.Substring(0, calorieString.Length - EntrySep.Length);
            data["calories"] = calorieString;

            return data;
        }
 
        public void FromDictionary(Dictionary<string, string> data)
        {
            string[] wrktStrs = data["workouts"].Split(EntrySep);
            
            // .Split will return an array with one empty string if the string is empty
            // so we need to check for that. `data["workouts"]` will be empty if the user
            // has no workouts.
            if (wrktStrs[0] != String.Empty)
                foreach (var str in wrktStrs)
                {
                    string[] split = str.Split(EntryValueSep);
                    Workout.Workout workout = new Workout.Workout(split[1], Int32.Parse(split[2]),
                            Enum.Parse<WorkoutIntensity>(split[3]));
                    Entry<Workout.Workout> entry = new Entry<Workout.Workout>(DateTime.Parse(split[0]), workout);
                    Workouts.Add(entry);
                }
                
            string[] wtStrs = data["weights"].Split(EntrySep);
            
            // The user will always have at least one weight entry, so we don't need to check
            // for an empty string.
            foreach (var str in wtStrs)
            {
                string[] split = str.Split(EntryValueSep);
                Entry<double> entry = new Entry<double>(DateTime.Parse(split[0]), double.Parse(split[1]));
                Weights.Add(entry);
            }
            
            string[] mealsStrs = data["meals"].Split(EntrySep);
            
            // Same as with workouts, we need to check for an empty string.
            if (mealsStrs[0] != String.Empty)
                foreach (var str in mealsStrs)
                {
                    string[] split = str.Split(EntryValueSep);
                    Entry<Meal> entry = new Entry<Meal>(DateTime.Parse(split[0]), _foodController.GetMeal(split[1]));
                    Meals.Add(entry);
                }
            
            string[] calStrs = data["calories"].Split(EntrySep);
            
            // Same as with workouts, we need to check for an empty string.
            if (calStrs[0] != String.Empty)
                foreach (var str in calStrs)
                {
                    string[] split = str.Split(EntryValueSep);
                    CalorieTracker calorieTracker = new CalorieTracker(double.Parse(split[1]), double.Parse(split[2]));
                    Entry<CalorieTracker> entry = new Entry<CalorieTracker>(DateTime.Parse(split[0]), calorieTracker);
                    Calories.Add(entry);
                }
        }

        public override bool Equals(object obj)
        {
            HistoryObject other = obj as HistoryObject;
            
            return other is not null && Equals(Workouts, other.Workouts) && Equals(Weights, other.Weights) && Equals(Meals, other.Meals) && Equals(Calories, other.Calories);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Workouts, Weights, Meals, Calories);
        }
    }
    
    
    /// <summary>
    /// data class used for the calories history
    /// </summary>
    /// <param name="ActualCalories">number of calories the user ate that day</param>
    /// <param name="TargetCalories">number of calories the user was recommended to eat that day</param>
    public record CalorieTracker(double ActualCalories, double TargetCalories);
}
