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
        Load();
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

    public Goal GetGoalBasedOnWeightDifference(double targetWeight)
    {
        var targetMinusCurrent = targetWeight - app.HistoryControl.CurrentWeight;
        return targetMinusCurrent switch
        {
            < -5 => new LoseWeightGoal(this, targetWeight),
            <= 5 => new MaintainWeightGoal(this, targetWeight),
            _ => new GainWeightGoal(this, targetWeight)
        };
    }

    public void Save()
    {
        // Write the goal to a JSON file for persistence
        var goalData = Goal.ToDictionary;
        goalData["isFitness"] = Goal is FitnessGoal;
        File.WriteAllText(goalsPath, JsonConvert.SerializeObject(goalData));
    }
    public void Load()
    {
        // Don't do anything if data files don't exist yet (e.g. first startup)
        if (!File.Exists(goalsPath))
            return;

        // Read the goal from a JSON file for persistence
        var goalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(goalsPath));
        var weightGoal = (double)goalDict["weightGoal"];
        var isFitness = (bool)goalDict["isFitness"];

        var goal = GetGoalBasedOnWeightDifference(weightGoal);
        if (isFitness)
            goal = new FitnessGoal(goal, app.GetRecommendedWorkouts());
        Goal = goal;
    }
}