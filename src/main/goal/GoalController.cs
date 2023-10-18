namespace NutriApp.Goal;

using System;
using NutriApp;
using NutriApp.Food;

public class GoalController
{
    private readonly App app;
    private Goal goal;

    public Goal Goal { get; set; }
    public void SetGoalByIndex(int index, int weight) {
        Goal = index switch
        {
            0 =>  new LoseWeightGoal(this, weight),
            1 => new MaintainWeightGoal(this, weight),
            2 => new GainWeightGoal(this, weight),
            _ => throw new Exception("Invalid goal index")
        };
    }

    public GoalController(App app)
    {
        this.app = app;
    }

    public void IncorporateFitness() { 
        var workouts = app.GetRecommendedWorkouts();
        goal.IncorporateFitness(workouts);
    }
    
    public void CompareUserWeightToGoal() { 
        goal.CheckWeight(app.User.GetWeight);
    }

    public delegate void PassedCalorieGoalHandler();
    public event PassedCalorieGoalHandler PassedCalorieGoalEvent;

    public void ConsumeMealHandler(Meal _)
    {
        if (CheckUserPassedCalorieGoal())
            PassedCalorieGoalEvent?.Invoke();
    }

    public bool CheckUserPassedCalorieGoal() {
        double todaysCalories = app.GetTodaysCalories();
        double calorieGoal = goal.DailyCalorieGoal;

        // can adjust as needed
        double marginOfError = 100;

        return todaysCalories > calorieGoal + marginOfError;
    }

    private void Save() { }
    private void Load() { }
}