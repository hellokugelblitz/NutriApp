using System.Collections.Generic;
using NutriApp.Food;

namespace NutriAppTest;

[TestClass]
public class TestShoppingList
{
    [TestMethod]
    public void TestAddToShoppingList()
    {
        // Setup
        Ingredient ingredientOne = new Ingredient("egg", 1, 1, 1, 1, 1);
        ShoppingList list = new ShoppingList();

        // Invoke
        list.AddItem(ingredientOne, 1);

        // Create the expected dictionary
        Dictionary<Ingredient, double> expected = new Dictionary<Ingredient, double>
        {
            { ingredientOne, 1 }
        };

        // Get the actual shopping list contents
        Dictionary<Ingredient, double> actual = list.List;

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }
}