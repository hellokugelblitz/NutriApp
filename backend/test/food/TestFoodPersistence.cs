using Moq;
using NutriApp.Food;
using System.IO;
using System.Linq;

namespace NutriAppTest;

[TestClass]
public class TestFoodPersistence
{
    [TestMethod]
    public void TestSave()
    {
        File.Delete($"{Persistence.FoodDataPath}\\ingredient_stock.json");
        File.Delete($"{Persistence.FoodDataPath}\\recipes.json");
        File.Delete($"{Persistence.FoodDataPath}\\meals.json");

        FoodController food = new FoodController(new Mock<App>(2).Object);

        Recipe recipe1 = new Recipe("butter and cheese??");
        recipe1.AddInstruction("Add some butter");
        recipe1.AddInstruction("Add a BRICK OF CHEESE");
        recipe1.AddInstruction("Profit");
        recipe1.AddChild(food.GetIngredient("BUTTER,WITH SALT"), 2);
        recipe1.AddChild(food.GetIngredient("CHEESE,BRICK"), 1);
        food.AddRecipe(recipe1);

        Recipe recipe2 = new Recipe("3 Ice Cream Sandwiches");
        recipe2.AddInstruction("Buy three ice cream sandwiches");
        recipe2.AddChild(food.GetIngredient("ICE CRM SNDWCH"), 3);
        food.AddRecipe(recipe2);

        Recipe recipe3 = new Recipe("Sadness");
        recipe3.AddInstruction("Take one (1) dried egg");
        recipe3.AddInstruction("Cover it in salt");
        recipe3.AddChild(food.GetIngredient("EGG,WHL,DRIED"), 1);
        recipe3.AddChild(food.GetIngredient("SALT,TABLE"), 5);
        food.AddRecipe(recipe3);

        Meal meal1 = new Meal("Dinner");
        meal1.AddChild(food.GetRecipe("butter and cheese??"), 1);
        meal1.AddChild(food.GetRecipe("3 Ice Cream Sandwiches"), 1);
        food.AddMeal(meal1);

        food.Save();

        string expectedRecipeJson = "[{\"preparationInstructions\":[\"Add some butter\",\"Add a BRICK OF CHEESE\",\"Profit\"],\"name\":\"butter and cheese??\",\"children\":{\"BUTTER,WITH SALT\":2.0,\"CHEESE,BRICK\":1.0}},{\"preparationInstructions\":[\"Buy three ice cream sandwiches\"],\"name\":\"3 Ice Cream Sandwiches\",\"children\":{\"ICE CRM SNDWCH\":3.0}},{\"preparationInstructions\":[\"Take one (1) dried egg\",\"Cover it in salt\"],\"name\":\"Sadness\",\"children\":{\"EGG,WHL,DRIED\":1.0,\"SALT,TABLE\":5.0}}]";
        string actualRecipeJson = File.ReadAllText($"{Persistence.FoodDataPath}\\recipes.json");
        Assert.AreEqual(expectedRecipeJson, actualRecipeJson);

        string expectedMealJson = "[{\"name\":\"Dinner\",\"children\":{\"butter and cheese??\":1.0,\"3 Ice Cream Sandwiches\":1.0}}]";
        string actualMealJson = File.ReadAllText($"{Persistence.FoodDataPath}\\meals.json");
        Assert.AreEqual(expectedMealJson, actualMealJson);
    }

    [TestMethod]
    public void TestLoad()
    {
        string ingredientJson = "{\"BUTTER,WITH SALT\":1.0,\"CHEESE,BRICK\":2.0,\"ICE CRM SNDWCH\":8.0,\"EGG,WHL,DRIED\":12.0,\"SALT,TABLE\":50.0}";
        string recipeJson = "[{\"preparationInstructions\":[\"Add some butter\",\"Add a BRICK OF CHEESE\",\"Profit\"],\"name\":\"butter and cheese??\",\"children\":{\"BUTTER,WITH SALT\":2.0,\"CHEESE,BRICK\":1.0}},{\"preparationInstructions\":[\"Buy three ice cream sandwiches\"],\"name\":\"3 Ice Cream Sandwiches\",\"children\":{\"ICE CRM SNDWCH\":3.0}},{\"preparationInstructions\":[\"Take one (1) dried egg\",\"Cover it in salt\"],\"name\":\"Sadness\",\"children\":{\"EGG,WHL,DRIED\":1.0,\"SALT,TABLE\":5.0}}]";
        string mealJson = "[{\"name\":\"Dinner\",\"children\":{\"butter and cheese??\":1.0,\"3 Ice Cream Sandwiches\":1.0}}]";

        File.WriteAllText($"{Persistence.FoodDataPath}\\ingredient_stock.json", ingredientJson);
        File.WriteAllText($"{Persistence.FoodDataPath}\\recipes.json", recipeJson);
        File.WriteAllText($"{Persistence.FoodDataPath}\\meals.json", mealJson);

        FoodController food = new FoodController(new Mock<App>(2).Object);  // loading happens here

        // Correct number of records loaded
        Assert.AreEqual(3, food.Recipes.Length);
        Assert.AreEqual(1, food.Meals.Length);

        // Correct ingredient stock
        Assert.AreEqual(1, food.GetIngredient("BUTTER,WITH SALT").Stock);
        Assert.AreEqual(2, food.GetIngredient("CHEESE,BRICK").Stock);
        Assert.AreEqual(50, food.GetIngredient("SALT,TABLE").Stock);

        // Correct recipe info
        Recipe recipe = food.GetRecipe("butter and cheese??");
        Assert.IsNotNull(recipe);

        Assert.AreEqual("butter and cheese??", recipe.Name);
        Assert.AreEqual(3, recipe.Instructions.Length);
        Assert.AreEqual(2, recipe.Children.Count);

        // Correct meal info
        Meal meal = food.GetMeal("Dinner");
        Assert.IsNotNull(meal);

        Assert.AreEqual("Dinner", meal.Name);
        Assert.AreEqual(2, meal.Children.Count);
        Assert.AreEqual(3, meal.Ingredients.Count);
    }
}