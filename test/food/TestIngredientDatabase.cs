using NutriApp.Food;

namespace NutriAppTest;

[TestClass]
public class TestIngredientDatabase
{
    [TestMethod]
    public void TestLoadIngredients()
    {
        IngredientDatabase db = new InMemoryIngredientDatabase();

        int expectedIngredients = 8790;
        int actualIngredients = db.GetAll().Length;
        Assert.AreEqual(expectedIngredients, actualIngredients);
    }

    [TestMethod]
    public void TestGetIngredient()
    {
        IngredientDatabase db = new InMemoryIngredientDatabase();

        // Should be case insensitive

        double expectedCalories = 371;

        Ingredient ingredient = db.Get("CHEESE,BRICK");
        Assert.AreEqual(expectedCalories, ingredient.Calories);

        ingredient = db.Get("  cheese,Brick");
        Assert.AreEqual(expectedCalories, ingredient.Calories);
    }
}