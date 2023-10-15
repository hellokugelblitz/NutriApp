using System.Collections.Generic;

namespace NutriApp.Food;

public class FoodController
{
    private List<Recipe> recipes;
    private List<Meal> meals;
    // private ShoppingList shoppingList;
    private App app;

    public FoodController(App app) {}

    private void Save() {}
    
    private void Load() {}

    public void AddRecipe(string name) {}

    public Recipe GetRecipe(string name) { return null; }

    public void AddMeal(string name) {}

    public Meal GetMeal(string name) { return null; }

    public void ConsumeMeal(string name) {}

    public void AddIngredientStock(string name, double quantity) {}

    public delegate void MealEventHandler(Meal meal);
    public event MealEventHandler MealConsumeEvent;
}