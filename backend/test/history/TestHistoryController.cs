using System;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using NutriApp.Food;
using NutriApp.Goal;
using NutriApp.History;
using NutriApp.Workout;

namespace NutriAppTest.history;

[TestClass]
public class TestHistoryController
{
    private App _app;
    private HistoryController history;
    public void Setup()
    {
        // _app = new App(1/150f);//1/90
        // history = _app.HistoryControl;
        // _app.GoalControl.Goal = new MaintainWeightGoal(_app.GoalControl, 155);
        //
        // Recipe recipe = new Recipe("mac and cheese");
        // recipe.AddInstruction("bake them kids");
        // recipe.AddChild(new Ingredient("cheese", 150, 1.5d, 1.5d, 1.5d, 1.5d), 3);
        // recipe.AddChild(new Ingredient("noodles", 75, 1, 1, 1, 1), 1);
        // _app.FoodControl.AddRecipe(recipe);
        //
        // Meal meal = new Meal("mac");
        // meal.AddChild(recipe, 2);
        // _app.FoodControl.AddMeal(meal);
        //
        // Workout workout = new Workout("run", 30, WorkoutIntensity.MEDIUM);
        // history.AddMeal(_app.FoodControl.GetMeal("mac"));
        // history.AddMeal(_app.FoodControl.GetMeal("mac"));
        // history.AddWorkout(workout);
        //
        // history.AddCalories(_app.TimeStamp);
        //
        // history.SetWeight(150);

        
    }

    [TestMethod]
    public void TestGetCalorieCount()
    {
        // Setup();
        //
        //     Console.WriteLine(_app.FoodControl.GetMeal("mac").Calories); //test for a day with 2 meals
        //     Assert.AreEqual(history.GetCalorieCount(_app.TimeStamp), 450);
    }
    
    
}