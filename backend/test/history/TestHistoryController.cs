using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using NutriApp.Food;
using NutriApp.Goal;
using NutriApp.History;
using NutriApp.Save;
using NutriApp.Workout;

namespace NutriAppTest.history;

[TestClass]
public class TestHistoryController
{
    private const string PATH = "data\\saves";

    private App _app;
    private HistoryController history;
    private User testUser = new User("dannytga", "danny", 72, DateTime.Now, "imm tallll");
    private Guid dannyGuid;
    
    public void Setup()
    {
        _app = new App(1/150f);//1/90
        history = _app.HistoryControl;
        var data = _app.UserControl.CreateUser(testUser, "hi");
        dannyGuid = data.Item1;
        _app.GoalControl.SetGoal(new MaintainWeightGoal(_app.GoalControl, 155, testUser.UserName), testUser.UserName);
        
        Recipe recipe = new Recipe("mac and cheese");
        recipe.AddInstruction("bake them kids");
        recipe.AddChild(_app.FoodControl.GetIngredient("CHEESE,BRICK"), 3);
        recipe.AddChild(_app.FoodControl.GetIngredient("PASTA,DRY,ENR"), 1);
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
    
        Assert.AreEqual(history.GetCalorieCount(testUser.UserName), 1484);
    }

    [TestMethod]

    public void TestSaveLoad()
    {
        Setup();
        ClearDirectory();
        var saveSystem = _app.SaveSyst;

        var adapter = new JSONAdapter();
        saveSystem.SetFileType(adapter);

        var previousData = (history.GetMeals(testUser.UserName),
            history.GetCalories(testUser.UserName), history.GetWorkouts(testUser.UserName),
            history.Weights(testUser.UserName));
        _app.UserControl.Logout(dannyGuid);
        var loginData = _app.UserControl.Login(testUser.UserName, "hi");
        var newName = loginData.Item2.UserName;
        var newData = (history.GetMeals(newName),
            history.GetCalories(newName), history.GetWorkouts(newName),
            history.Weights(newName));

        Assert.IsTrue(previousData.Item1.SequenceEqual(newData.Item1));
        Assert.IsTrue(previousData.Item2.SequenceEqual(newData.Item2));
        Assert.IsTrue(previousData.Item3.SequenceEqual(newData.Item3));
        Assert.IsTrue(previousData.Item4.SequenceEqual(newData.Item4));
    }
    
    /// <summary>
    /// clears the directory and creates a new one at the PATH constant
    /// </summary>
    private void ClearDirectory()
    {
        if(Directory.Exists(PATH))
        {
            Directory.Delete(PATH, true);
        }

        Directory.CreateDirectory(PATH);
    }
}