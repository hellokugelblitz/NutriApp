using System;
using System.IO;
using System.Linq;
using NutriApp.Food;
using NutriApp.Goal;
using NutriApp.Save;
using NutriApp.Workout;

namespace NutriAppTest;

[TestClass]
public class TestFoodController
{
    private const string PATH = "data\\saves";

    private App _app;
    private FoodController food;
    private User testUser = new User("dannytga", "danny", 72, DateTime.Now, "imm tallll");
    private Guid dannyGuid;
    [TestMethod]
    public void TestSaveLoadController()
    {
        ClearDirectory();
        Setup();

        var recipes = food.Recipes;
        var meals = food.Meals;
        food.SaveController();
        ISaveSystem saveSystem = new SaveSystem();
        saveSystem.SetFileType(new JSONAdapter());
        food = new FoodController(null, saveSystem);
        food.LoadController();
        
        Assert.IsTrue(recipes.SequenceEqual(food.Recipes));
        Assert.IsTrue(meals.SequenceEqual(food.Meals));
    }

    public void Setup()
    {
        _app = new App(1/150f);//1/90
        food = _app.FoodControl;
        _app.SaveSyst.SubscribeSaveable(_app.UserControl);
        _app.SaveSyst.SubscribeSaveable(food);
        var data = _app.UserControl.CreateUser(testUser, "hi");
        dannyGuid = data.Item1;
        _app.GoalControl.SetGoal(new MaintainWeightGoal(_app.GoalControl, 155, testUser.UserName), testUser.UserName);
        
        Recipe recipe = new Recipe("mac and cheese");
        recipe.AddInstruction("bake them kids");
        recipe.AddChild(food.GetIngredient("CHEESE,BRICK"), 3);
        recipe.AddChild(food.GetIngredient("PASTA,DRY,ENR"), 1);
        _app.FoodControl.AddRecipe(recipe);
        
        Meal meal = new Meal("mac");
        meal.AddChild(recipe, 2);
        food.AddMeal(meal);
        
        food.EditIngredientStock("CHEESE,BRICK", 3, testUser.UserName);
    }

    [TestMethod]
    public void TestExport() {
        var app = new App(1);
        char entrySep = ':';
        char valueSep = '|';
        char ingredientSep = '^';

        var key = "bake them kids|CHEESE,BRICK:3^PASTA,DRY,ENR:1";

        Recipe recipe = new Recipe(key);
        var splitValues = key.Split(valueSep);
        var instructions = splitValues[0].Split(entrySep);
        foreach(var instruction in instructions) {
            recipe.AddInstruction(instruction);
        }
        var ingredientPair = splitValues[1].Split(ingredientSep);
        foreach(var pair in ingredientPair) {
            var split = pair.Split(entrySep);
            recipe.AddChild(app.FoodControl.GetIngredient(split[0]), Int32.Parse(split[1]));
        }

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