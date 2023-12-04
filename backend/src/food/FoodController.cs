using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using NutriApp.Save;

namespace NutriApp.Food;

/// <summary>
/// High-level controller responsible for food-related operations.
/// </summary>
public class FoodController : ISaveableController
{
    private readonly string ingredientStockPath = $"{Persistence.FoodDataPath}\\ingredient_stock.json";
    private readonly string recipePath = $"{Persistence.FoodDataPath}\\recipes.json";
    private readonly string mealPath = $"{Persistence.FoodDataPath}\\meals.json";

    private List<Recipe> recipes;
    private List<Meal> meals;

    // Key: username
    // Value: data class representing remaining ingredient stock
    private Dictionary<string, IngredientStocks> ingredientStocks = new Dictionary<string, IngredientStocks>();

    private ShoppingListController shoppingList;
    private ShoppingListCriteria recipeCriteria;
    private App app;
    private IngredientDatabase ingredientDatabase;

    /// <summary>
    /// Retrieves all recipes any user has created.
    /// </summary>
    public Recipe[] Recipes => recipes.ToArray();

    /// <summary>
    /// Retrieves all meals any user has created.
    /// </summary>
    public Meal[] Meals => meals.ToArray();

    public FoodController(App app)
    {
        this.app = app;
        ingredientDatabase = new InMemoryIngredientDatabase();
        shoppingList = new ShoppingListController();
        recipeCriteria = new SpecificRecipeCriteria(shoppingList, this);

        ingredientStocks = new Dictionary<string, IngredientStocks>();
        recipes = new List<Recipe>();
        meals = new List<Meal>();

        shoppingList.SetCriteria(recipeCriteria);

        foreach (string username in ingredientStocks.Keys)
            shoppingList.Update(Recipes, username);
    }

    /// <summary>
    /// Adds a recipe with some pre-configured attributes to the user's saved recipes.
    /// </summary>
    public void AddRecipe(Recipe recipe)
    {
        recipes.Add(recipe);

        foreach (string username in ingredientStocks.Keys)
            shoppingList.Update(Recipes, username);
    }

    public void RemoveRecipe(Recipe recipe)
    {
        recipes.Remove(recipe);

        foreach (string username in ingredientStocks.Keys)
            shoppingList.Update(Recipes, username);
    }

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
    /// Adds a meal with some pre-configured attributes to the user's saved meals.
    /// </summary>
    public void AddMeal(Meal meal) => meals.Add(meal);

    /// <summary>
    /// Removes a meal
    /// </summary>
    public void RemoveMeal(Meal meal) => meals.Remove(meal);

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
    /// Returns stocks for all ingredients that the given user has more than zero of.
    /// </summary>
    public IngredientStocks GetAllIngredientStocks(string username) => ingredientStocks[username];

    /// <summary>
    /// Returns how much of an ingredient the given user has.
    /// </summary>
    public double GetSingleIngredientStock(string ingredientName, string username) => ingredientStocks[username].GetIngredientStock(ingredientName);

    /// <summary>
    /// Checks if the given user has enough ingredients in stock for a given meal
    /// </summary>
    public bool EnoughIngredients(string mealName, string username)
    {
        Meal mealConsumed = GetMeal(mealName);

        // Check ingredient stock
        foreach (Ingredient ingredient in mealConsumed.Ingredients.Keys)
        {
            double requiredStock = mealConsumed.Ingredients[ingredient];

            if (ingredientStocks[username].GetIngredientStock(ingredient.Name) < requiredStock)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Consumes a meal if there is enough ingredient stock. Returns true if meal consumption
    /// was successful, false otherwise.
    /// </summary>
    public bool ConsumeMeal(string mealName, string username)
    {
        Meal mealConsumed = GetMeal(mealName);

        // Check ingredient stock
        if (!EnoughIngredients(mealName, username)) return false;

        // Meal consumed successfully
        MealConsumeEvent?.Invoke(mealConsumed, username);

        // Remove ingredient stock
        foreach (Ingredient ingredient in mealConsumed.Ingredients.Keys)
        {
            double requiredStock = mealConsumed.Ingredients[ingredient];
            EditIngredientStock(ingredient.Name, -requiredStock, username);
        }

        shoppingList.Update(Recipes, username);
        return true;
    }

    /// <summary>
    /// Edits the given user's stock of a certain ingredient, where change is positive
    /// if the user bought ingredients, and negative if ingredients were consumed.
    /// </summary>
    public void EditIngredientStock(string ingredientName, double change, string username)
    {
        double newStock = ingredientStocks[username].GetIngredientStock(ingredientName) + change;

        if (newStock <= 0)
            ingredientStocks[username].RemoveEntry(ingredientName);
        else
            ingredientStocks[username].SetEntry(ingredientName, newStock);
    }

    public delegate void MealEventHandler(Meal meal, string username);
    public event MealEventHandler MealConsumeEvent;

    public void AddNewUser(User user)
    {
        // Create a new entry for ingredient stock
        // Call the ShoppingListController to add that user to the shopping list dictionary
    }

    public void SaveUser(string username)
    {
        // Save ingredient stocks for a user
    }

    public void LoadUser(string username)
    {
        // Load ingredient stocks for a user
    }

    public void SaveController()
    {
        // Save recipes and meals
    }

    public void LoadController()
    {
        // Load recipes and meals
    }
}

public class IngredientStocks
{
    public Dictionary<string, double> stocks { get; private set; }

    public IngredientStocks()
    {
        stocks = new Dictionary<string, double>();
    }

    /// <summary>
    /// Upserts an entry into the dictionary of ingredient stocks.
    /// </summary>
    public void SetEntry(string ingredientName, double amount)
    {
        if (!stocks.ContainsKey(ingredientName))
            stocks.Add(ingredientName, 0);

        stocks[ingredientName] = amount;
    }

    /// <summary>
    /// Removes the entry for the given ingredient.
    /// </summary>
    public void RemoveEntry(string ingredientName)
    {
        if (!stocks.ContainsKey(ingredientName)) return;

        stocks.Remove(ingredientName);
    }

    /// <summary>
    /// Gets the stock for the given ingredient. If there is no record for the
    /// ingredient, we assume the user has none.
    /// </summary>
    public double GetIngredientStock(string ingredientName)
    {
        if (!stocks.ContainsKey(ingredientName)) return 0;

        return stocks[ingredientName];
    }
}