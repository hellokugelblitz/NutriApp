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
    private readonly ISaveSystem saveSystem;

    private Dictionary<string, Goal> goals = new();

    public Goal GetGoal(string username) => goals[username];
    public void SetGoal(Goal goal, string username) => goals[username] = goal;
    public GoalController(App app, ISaveSystem saveSystem)
    {
        this.app = app;
        this.saveSystem = saveSystem;
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
    public void WeightChangedHandler(double weight, string username)
    {
        goals[username] = GetGoal(username).CheckWeight(weight);
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

    public void SaveUser(string folderName)
    {
        string username = SaveSystem.GetUsernameFromFile(folderName);
        saveSystem.GetFileSaver().Save(SaveSystem.GetFullPath(folderName,"goal"), goals[username].ToDictionary());
        goals.Remove(username);
    }

    public void LoadUser(string folderName)
    {
        Dictionary<string, string> data = saveSystem.GetFileSaver().Load(SaveSystem.GetFullPath(folderName, "goal"));
        string username = SaveSystem.GetUsernameFromFile(folderName);
        
        var weightGoal = double.Parse(data["WeightGoal"]);
        var isFitness = data.ContainsKey("isFitness");
        
        var goal = GetGoalBasedOnWeightDifference(weightGoal, username);
        
        if (isFitness)
            goal = new FitnessGoal(goal, app.GetRecommendedWorkouts(username));
        goals[username] = goal;
    }

    public void SaveController()
    { }

    public void LoadController()
    { }

    public void AddNewUser(User user)
    {
        if(goals.ContainsKey(user.UserName)) return;
        goals[user.UserName] = new DefaultGoal();
    }
}