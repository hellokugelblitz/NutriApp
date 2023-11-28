using NutriApp.Save;

namespace NutriApp.Goal;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.IO;
using NutriApp;
using NutriApp.Food;

public class GoalController : ISaveableController
{
    private readonly string goalsPath = $"{Persistence.GoalsDataPath}\\goals.json";

    private readonly App app;

    private Dictionary<string, Goal> goals = new();

    public Goal GetGoal(string username) => goals[username];
    public void SetGoal(Goal goal, string username) => goals[username] = goal;
    public GoalController(App app)
    {
        this.app = app;
        //Load();
    }

    /// <summary>
    /// Incorporates fitness into the user's goal.
    /// </summary>
    public void IncorporateFitness(string username)
    {
        var workouts = app.GetRecommendedWorkouts(username);
        GetGoal(username).IncorporateFitness(workouts);
    }

    /// <summary>
    /// Checks the user's weight against their weight goal.
    /// May switch the goal if the user has reached certain conditions, like
    /// completing a goal or deviating from the goal.
    /// </summary>
    public void CompareUserWeightToGoal(string username)
    {
        goals[username] = GetGoal(username).CheckWeight(app.HistoryControl.CurrentWeight(username));
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
    /// <param name="username">username of user</param>
    public void ConsumeMealHandler(Meal _, string username)
    {
        if (CheckUserExceededCalorieGoal(username))
            PassedCalorieGoalEvent?.Invoke();
    }

    /// <summary>
    /// Checks if the user has exceeeded their daily calorie goal.
    /// </summary>
    /// <returns>Whether the user exceeded their daily calorie goal.</returns>
    public bool CheckUserExceededCalorieGoal(string username)
    {
        
        double todaysCalories = app.GetTodaysCalories();
        double calorieGoal = GetGoal(username).DailyCalorieGoal;

        // can adjust as needed
        double marginOfError = 100;

        return todaysCalories > calorieGoal + marginOfError;
    }

    /// <summary>
    /// Gets the goal based on the difference between the user's current weight
    /// and their target weight.
    /// <list type="bullet">
    /// <item>If the difference is less than -5, the user wants to lose weight</item>
    /// <item>If the difference is greater than 5, the user wants to gain weight</item>
    /// <item>Else, the user wants to maintain their weight</item>
    /// </list>
    /// </summary>
    /// <param name="targetWeight">The user's target weight</param>
    /// <param name="username">username of user</param>
    /// <returns>The Goal instance</returns>
    public Goal GetGoalBasedOnWeightDifference(double targetWeight, string username)
    {
        var targetMinusCurrent = targetWeight - app.HistoryControl.CurrentWeight(username);
        return targetMinusCurrent switch
        {
            < -5 => new LoseWeightGoal(this, targetWeight, username),
            > 5 => new GainWeightGoal(this, targetWeight, username),
            _ => new MaintainWeightGoal(this, targetWeight, username)
        };
    }

    /// <summary>
    /// Sets the goal based on the difference between the user's current weight
    /// and their target weight. See <c>GetGoalBasedOnWeightDifference</c>
    /// for further details.
    /// </summary>
    /// <param name="targetWeight">The user's target weight</param>
    /// <param name="username">username of user</param>
    /// <seealso cref="GetGoalBasedOnWeightDifference"/>
    public void SetGoalBasedOnWeightDifference(double targetWeight, string username)
    {
        goals[username] = GetGoalBasedOnWeightDifference(targetWeight, username);
    }

    // public void Save()
    // {
    //     // Write the goal to a JSON file for persistence
    //     var goalData = Goal.ToDictionary;
    //     goalData["isFitness"] = Goal is FitnessGoal;
    //     File.WriteAllText(goalsPath, JsonConvert.SerializeObject(goalData));
    // }
    // public void Load()
    // {
    //     // Don't do anything if data files don't exist yet (e.g. first startup)
    //     if (!File.Exists(goalsPath))
    //         return;
    //
    //     // Read the goal from a JSON file for persistence
    //     var goalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(goalsPath));
    //     var weightGoal = (double)goalDict["weightGoal"];
    //     var isFitness = (bool)goalDict["isFitness"];
    //
    //     var goal = GetGoalBasedOnWeightDifference(weightGoal);
    //     if (isFitness)
    //         goal = new FitnessGoal(goal, app.GetRecommendedWorkouts());
    //     Goal = goal;
    // }

    public void SaveUser(string folderName)
    {
        throw new System.NotImplementedException();
    }

    public void LoadUser(string folderName)
    {
        throw new System.NotImplementedException();
    }

    public void SaveController()
    {
        throw new System.NotImplementedException();
    }

    public void LoadController()
    {
        throw new System.NotImplementedException();
    }
}