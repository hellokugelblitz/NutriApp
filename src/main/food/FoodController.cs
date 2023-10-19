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

    /// <summary>
    /// Retrieves all recipes the user has created.
    /// </summary>
    public Recipe[] Recipes => recipes.ToArray();

    /// <summary>
    /// Retrieves all meals the user has created.
    /// </summary>
    public Meal[] Meals => meals.ToArray();

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
    /// Adds a blank recipe with the given name to the user's saved recipes.
    /// </summary>
    public void AddRecipe(string name) => recipes.Add(new Recipe(name));
    
    /// <summary>
    /// Adds a recipe with some pre-configured attributes to the user's saved recipes.
    /// </summary>
    public void AddRecipe(Recipe recipe) => recipes.Add(recipe);

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
    /// Adds a blank meal with the given name to the user's saved meals.
    /// </summary>
    public void AddMeal(string name) => meals.Add(new Meal(name));
    
    /// <summary>
    /// Adds a meal with some pre-configured attributes to the user's saved meals.
    /// </summary>
    public void AddMeal(Meal meal) => meals.Add(meal);

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
    /// Gets a single ingredient by its unique name.
    /// </summary>
    public Ingredient GetIngredient(string name) => ingredientDatabase.Get(name);

    /// <summary>
    /// Gets all ingredients whose names contain the given search term.
    /// </summary>
    public Ingredient[] SearchIngredients(string term) => ingredientDatabase.Search(term);

    /// <summary>
    /// Consumes a meal if there is enough ingredient stock. Returns true if meal consumption
    /// was successful, false otherwise.
    /// </summary>
    public bool ConsumeMeal(string name)
    {
        Meal mealConsumed = GetMeal(name);

        // Check ingredient stock
        foreach (Ingredient ingredient in mealConsumed.Ingredients.Keys)
        {
            double requiredStock = mealConsumed.Ingredients[ingredient];

            if (ingredient.Stock < requiredStock)
                return false;
        }

        // Meal consumed successfully
        MealConsumeEvent?.Invoke(mealConsumed);

        // Remove ingredient stock
        foreach (Ingredient ingredient in mealConsumed.Ingredients.Keys)
        {
            double requiredStock = mealConsumed.Ingredients[ingredient];
            ingredient.Stock -= requiredStock;
        }

        foreach (Recipe recipe in mealConsumed.Children.Keys)
            shoppingList.Update(recipe);

        return true;
    }

    /// <summary>
    /// Reports the purchase of new ingredients. Adds to ingredient stock and updates the
    /// shopping list.
    /// </summary>
    public void AddIngredientStock(string name, double quantity)
    {
        Ingredient ingredient = ingredientDatabase.Get(name);
        ingredient.Stock += quantity;

        shoppingList.AddItem(ingredient, quantity);
    }
    public delegate void MealEventHandler(Meal meal);
    public event MealEventHandler MealConsumeEvent;
}