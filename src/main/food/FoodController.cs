using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace NutriApp.Food;

/// <summary>
/// High-level controller responsible for food-related operations.
/// </summary>
public class FoodController
{
    private readonly string ingredientStockPath = $"{Persistence.FoodDataPath}\\ingredient_stock.json";
    private readonly string recipePath = $"{Persistence.FoodDataPath}\\recipes.json";
    private readonly string mealPath = $"{Persistence.FoodDataPath}\\meals.json";

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

    /// <summary>
    /// The shopping list representing which ingredients the user
    /// should purchase soon.
    /// </summary>
    public ShoppingList ShoppingList => shoppingList;

    public FoodController(App app)
    {
        this.app = app;
        ingredientDatabase = new InMemoryIngredientDatabase();
        shoppingList = new ShoppingList();

        recipes = new List<Recipe>();
        meals = new List<Meal>();

        app.DayEndEvent += (DateTime date) => Save();
        Load();
    }

    private void Save()
    {
        // Write each ingredient stock to a JSON file for persistence, since it isn't stored with
        // the other ingredient info. Also just store the ingredient name because it serializes better.
        Dictionary<string, double> ingredientStocks = new Dictionary<string, double>();

        foreach (Recipe recipe in recipes)
            foreach (Ingredient ingredient in recipe.Ingredients.Keys)
                ingredientStocks.Add(ingredient.Name, ingredient.Stock);

        string ingredientJson = JsonConvert.SerializeObject(ingredientStocks);
        File.WriteAllText(ingredientStockPath, ingredientJson);

        // Serialize each recipe
        List<SerializableRecipe> recipesToWrite = new List<SerializableRecipe>();

        foreach (Recipe recipe in recipes)
        {
            SerializableRecipe recipeToWrite = new SerializableRecipe(recipe.Name, recipe.Instructions);

            Dictionary<Ingredient, double>.KeyCollection ingredients = recipe.Children.Keys;

            foreach (Ingredient ingredient in ingredients)
                recipeToWrite.AddChild(ingredient.Name, recipe.Children[ingredient]);

            recipesToWrite.Add(recipeToWrite);
        }

        string recipeJson = JsonConvert.SerializeObject(recipesToWrite);
        File.WriteAllText(recipePath, recipeJson);

        // Serialize each meal
        List<SerializablePreparedFood> mealsToWrite = new List<SerializablePreparedFood>();

        foreach (Meal meal in meals)
        {
            SerializablePreparedFood mealToWrite = new SerializablePreparedFood(meal.Name);

            Dictionary<Recipe, double>.KeyCollection ingredients = meal.Children.Keys;

            foreach (Recipe recipe in ingredients)
                mealToWrite.AddChild(recipe.Name, meal.Children[recipe]);

            mealsToWrite.Add(mealToWrite);
        }

        string mealJson = JsonConvert.SerializeObject(mealsToWrite);
        File.WriteAllText(mealPath, mealJson);
    }
    
    private void Load()
    {
        // Don't do anything if data files don't exist yet (e.g. first startup)
        if (!File.Exists(ingredientStockPath) || !File.Exists(recipePath) || !File.Exists(mealPath))
            return;

        // Update ingredient stock
        string ingredientJson = File.ReadAllText(ingredientStockPath);
        Dictionary<string, double> ingredientStocks = JsonConvert.DeserializeObject<Dictionary<string, double>>(ingredientJson);
        
        foreach (KeyValuePair<string, double> ingredientStock in ingredientStocks)
        {
            string name = ingredientStock.Key;
            double stock = ingredientStock.Value;

            ingredientDatabase.Get(name).Stock = stock;
        }

        // Load recipes
        string recipeJson = File.ReadAllText(recipePath);
        SerializableRecipe[] loadedRecipes = JsonConvert.DeserializeObject<SerializableRecipe[]>(recipeJson);

        foreach (SerializableRecipe loadedRecipe in loadedRecipes)
        {
            Recipe recipe = new Recipe(loadedRecipe.Name);
            
            foreach (string step in loadedRecipe.Instructions)
                recipe.AddInstruction(step);

            foreach (KeyValuePair<string, double> ingredient in loadedRecipe.Children)
            {
                string name = ingredient.Key;
                double quantity = ingredient.Value;
                recipe.AddChild(ingredientDatabase.Get(name), quantity);
            }

            recipes.Add(recipe);
        }

        // Load meals
        string mealJson = File.ReadAllText(mealPath);
        SerializablePreparedFood[] loadedMeals = JsonConvert.DeserializeObject<SerializablePreparedFood[]>(mealJson);

        foreach (SerializablePreparedFood loadedMeal in loadedMeals)
        {
            Meal meal = new Meal(loadedMeal.Name);
            
            foreach (KeyValuePair<string, double> recipe in loadedMeal.Children)
            {
                string name = recipe.Key;
                double quantity = recipe.Value;
                meal.AddChild(GetRecipe(name), quantity);
            }

            meals.Add(meal);
        }
    }

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