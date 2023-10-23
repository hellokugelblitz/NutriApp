namespace NutriApp.Goal;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.IO;
using NutriApp;
using NutriApp.Food;

public class GoalController
{
    private readonly string goalsPath = $"{Persistence.GoalsDataPath}\\goals.json";

    private readonly App app;

    public Goal Goal { get; set; }

    public GoalController(App app)
    {
        this.app = app;
    }

    /// <summary>
    /// Incorporates fitness into the user's goal.
    /// </summary>
    public void IncorporateFitness()
    {
        var workouts = app.GetRecommendedWorkouts();
        Goal.IncorporateFitness(workouts);
    }

    /// <summary>
    /// Checks the user's weight against their weight goal.
    /// May switch the goal if the user has reached certain conditions, like
    /// completing a goal or deviating from the goal.
    /// </summary>
    public void CompareUserWeightToGoal()
    {
        Goal.CheckWeight(app.User.Weight);
    }

    /// <summary>
    /// Event for when the user has exceeded their daily calorie goal.
    /// </summary>
    public delegate void PassedCalorieGoalHandler();
    public event PassedCalorieGoalHandler PassedCalorieGoalEvent;

    /// <summary>
    /// Handles the event of the user consuming a meal.
    /// </summary>
    /// <param name="_">Required param to fulfill <c>MealEventHandler</c> signature</param>
    public void ConsumeMealHandler(Meal _)
    {
        if (CheckUserExceededCalorieGoal())
            PassedCalorieGoalEvent?.Invoke();
    }

    /// <summary>
    /// Checks if the user has exceeeded their daily calorie goal.
    /// </summary>
    /// <returns>Whether the user exceeded their daily calorie goal.</returns>
    public bool CheckUserExceededCalorieGoal()
    {
        double todaysCalories = app.GetTodaysCalories();
        double calorieGoal = Goal.DailyCalorieGoal;

        // can adjust as needed
        double marginOfError = 100;

        return todaysCalories > calorieGoal + marginOfError;
    }

    public void Save()
    {
        // Write the goal to a JSON file for persistence
        File.WriteAllText(goalsPath, JsonConvert.SerializeObject(Goal.ToDictionary));
    }
    public void Load()
    {
        // Don't do anything if data files don't exist yet (e.g. first startup)
        if (!File.Exists(goalsPath))
            return;

        // Read the goal from a JSON file for persistence
        var goalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(goalsPath));
        var type = (string)goalDict["type"];
        var weightGoal = (double)goalDict["weightGoal"];
        var dailyCalorieGoal = (double)goalDict["dailyCalorieGoal"];

        // Function to create a goal based on the type
        Goal GoalFromString(string type)
        {
            Goal goal = null;

            // Base goal types
            if (!type.Contains(';')) goal = type switch
            {
                "gain" => new GainWeightGoal(this, dailyCalorieGoal),
                "lose" => new LoseWeightGoal(this, dailyCalorieGoal),
                "maintain" => new MaintainWeightGoal(this, dailyCalorieGoal),
                _ => throw new System.Exception($"Invalid goal type: {type}")
            };

            // Decorated types
            else
            {
                var types = type.Split(new char[] { ';' }, 2);
                type = types[0];
                var decoratedTypes = types[1];
                goal = type switch
                {
                    "fitness" => new FitnessGoal(GoalFromString(decoratedTypes), app.GetRecommendedWorkouts()),
                    _ => throw new System.Exception($"Invalid goal type: {type}")
                };
            }

            return goal;
        }

        // Create the goal based on the type
        Goal = GoalFromString(type);
    }
}