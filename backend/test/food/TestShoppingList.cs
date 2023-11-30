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
        ShoppingListController shoppingListController = new ShoppingListController();
        Ingredient ingredientOne = new Ingredient("egg", 1, 1, 1, 1, 1);
        ShoppingList list = new ShoppingList();

        // Invoke
        shoppingListController.AddUser("test_user");
        shoppingListController.AddItem(ingredientOne, 1, "test_user");

        // Create the expected dictionary
        Dictionary<Ingredient, double> expected = new Dictionary<Ingredient, double>
        {
            { ingredientOne, 1 }
        };

        // Get the actual shopping list contents
        Dictionary<Ingredient, double> actual = shoppingListController.GetShoppingList("test_user").Entries;

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }
}