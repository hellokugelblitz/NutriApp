namespace NutriApp.Goal;

using System;
using NutriApp;
using NutriApp.Food;

public class GoalController
{
    private readonly App app;

    public Goal Goal { get; set; }

    public GoalController(App app)
    {
        this.app = app;
    }

    /// <summary>
    /// Incorporates fitness into the user's goal.
    /// </summary>
    public void IncorporateFitness() { 
        var workouts = app.GetRecommendedWorkouts();
        Goal.IncorporateFitness(workouts);
    }
    
    /// <summary>
    /// Checks the user's weight against their weight goal.
    /// May switch the goal if the user has reached certain conditions, like
    /// completing a goal or deviating from the goal.
    /// </summary>
    public void CompareUserWeightToGoal() { 
        Goal.CheckWeight(app.User.GetWeight);
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
    public bool CheckUserExceededCalorieGoal() {
        double todaysCalories = app.GetTodaysCalories();
        double calorieGoal = Goal.DailyCalorieGoal;

        // can adjust as needed
        double marginOfError = 100;

        return todaysCalories > calorieGoal + marginOfError;
    }

    private void Save() { }
    private void Load() { }
}