using System;
using System.Collections.Generic;
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
    private User testUser = new User("dannytga", "danny", 72, DateTime.Now, "imm tallll");

    public void Setup()
    {
        _app = new App(1/150f);//1/90
        history = _app.HistoryControl;
        _app.UserControl.CreateUser(testUser, "hi");
        _app.GoalControl.SetGoal(new MaintainWeightGoal(_app.GoalControl, 155, testUser.UserName), testUser.UserName);
        
        Recipe recipe = new Recipe("mac and cheese");
        recipe.AddInstruction("bake them kids");
        recipe.AddChild(new Ingredient("cheese", 150, 1.5d, 1.5d, 1.5d, 1.5d), 3);
        recipe.AddChild(new Ingredient("noodles", 75, 1, 1, 1, 1), 1);
        _app.FoodControl.AddRecipe(recipe);
        
        Meal meal = new Meal("mac");
        meal.AddChild(recipe, 2);
        _app.FoodControl.AddMeal(meal);
        
        Workout workout = new Workout("run", 30, WorkoutIntensity.MEDIUM);
        history.AddMeal(_app.FoodControl.GetMeal("mac"), testUser.UserName);
        history.AddMeal(_app.FoodControl.GetMeal("mac"), testUser.UserName);
        history.AddWorkout(workout, testUser.UserName);
        
        history.AddCalories(testUser.UserName);
        
        history.SetWeight(150, testUser.UserName);

        
    }

    [TestMethod]
    public void TestGetCalorieCount()
    {
        Setup();
    
        Assert.AreEqual(history.GetCalorieCount(testUser.UserName), 450);
    }
}