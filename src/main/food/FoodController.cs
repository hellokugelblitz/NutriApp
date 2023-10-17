using System.Collections.Generic;

namespace NutriApp.Food;

public class FoodController
{
    private List<Recipe> recipes;
    private List<Meal> meals;

    // private ShoppingList shoppingList;
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

    public void AddRecipe(string name) => recipes.Add(new Recipe(name));

    public Recipe GetRecipe(string name)
    {
        foreach (Recipe recipe in recipes)
            if (recipe.Name == name)
                return recipe;

        return null;
    }

    public void AddMeal(string name) => meals.Add(new Meal(name));

    public Meal GetMeal(string name)
    {
        foreach (Meal meal in meals)
            if (meal.Name == name)
                return meal;

        return null;
    }

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

    public void AddIngredientStock(string name, double quantity)
    {
        Ingredient ingredient = ingredientDatabase.Get(name);
        ingredient.Stock += quantity;

        // TODO: update shopping list
    }

    public delegate void MealEventHandler(Meal meal);
    public event MealEventHandler MealConsumeEvent;
}

public enum ConsumeMealResult { Success, NotEnoughStock, ExceedCalorieGoal }