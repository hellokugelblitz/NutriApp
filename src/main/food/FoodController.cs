using System.Collections.Generic;

namespace NutriApp.Food;

/// <summary>
/// High-level controller responsible for food-related operations.
/// </summary>
public class FoodController
{
    private List<Recipe> recipes;
    private List<Meal> meals;

    private ShoppingList shoppingList;
    private App app;
    private IngredientDatabase ingredientDatabase;

    public FoodController(App app)
    {
        this.app = app;
        ingredientDatabase = new InMemoryIngredientDatabase();

        recipes = new List<Recipe>();
        meals = new List<Meal>();
    }

    private void Save() {}
    
    private void Load() {}

    /// <summary>
    /// Adds a recipe with the given name to the user's saved recipes.
    /// </summary>
    public void AddRecipe(string name) => recipes.Add(new Recipe(name));

    /// <summary>
    /// Retrieves a recipe given its unique name. Returns null if there is no
    /// recipe with that name.
    /// </summary>
    public Recipe GetRecipe(string name)
    {
        foreach (Recipe recipe in recipes)
            if (recipe.Name == name)
                return recipe;

        return null;
    }

    /// <summary>
    /// Adds a meal with the given name to the user's saved meals.
    /// </summary>
    public void AddMeal(string name) => meals.Add(new Meal(name));

    /// <summary>
    /// Retrieves a meal given its unique name. Returns null if there is no
    /// meal with that name.
    /// </summary>
    public Meal GetMeal(string name)
    {
        foreach (Meal meal in meals)
            if (meal.Name == name)
                return meal;

        return null;
    }

    /// <summary>
    /// Consumes a meal if there is enough ingredient stock AND if it won't exceed the daily
    /// calorie goal; depletes ingredients and updates shopping list. Returns if meal consumption
    /// was successful.
    /// </summary>
    public ConsumeMealResult ConsumeMeal(string name)
    {
        Meal mealConsumed = GetMeal(name);

        // Check ingredient stock
        foreach (Ingredient ingredient in mealConsumed.Ingredients.Keys)
        {
            double requiredStock = mealConsumed.Ingredients[ingredient];

            if (ingredient.Stock < requiredStock)
                return ConsumeMealResult.NotEnoughStock;
        }

        // Check calorie goal
        if (mealConsumed.Calories > app.GoalControl.Goal.DailyCalorieGoal)
            return ConsumeMealResult.ExceedCalorieGoal;

        // Meal consumed successfully
        MealConsumeEvent?.Invoke(mealConsumed);

        // Remove ingredient stock
        foreach (Ingredient ingredient in mealConsumed.Ingredients.Keys)
        {
            double requiredStock = mealConsumed.Ingredients[ingredient];
            ingredient.Stock -= requiredStock;
        }

        // TODO: update shopping list
        return ConsumeMealResult.Success;
    }

    /// <summary>
    /// Reports the purchase of new ingredients. Adds to ingredient stock and updates the
    /// shopping list.
    /// </summary>
    public void AddIngredientStock(string name, double quantity)
    {
        Ingredient ingredient = ingredientDatabase.Get(name);
        ingredient.Stock += quantity;

        // TODO: update shopping list
    }

    public delegate void MealEventHandler(Meal meal);
    public event MealEventHandler MealConsumeEvent;
}

/// <summary>
/// The result of meal consumption. Success is meal was consumed successfully, and a
/// different value otherwise.
/// </summary>
public enum ConsumeMealResult { Success, NotEnoughStock, ExceedCalorieGoal }