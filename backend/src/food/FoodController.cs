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
    private readonly string recipePath = SaveSystem.SavePath + "\\recipes.json";
    private readonly string mealPath = SaveSystem.SavePath + "\\meals.json";

    private List<Recipe> recipes;
    private List<Meal> meals;

    // Key: username
    // Value: data class representing remaining ingredient stock
    private Dictionary<string, IngredientStocks> ingredientStocks = new Dictionary<string, IngredientStocks>();

    private ShoppingListController shoppingList;
    private ShoppingListCriteria recipeCriteria;
    private App app;
    private IngredientDatabase ingredientDatabase;
    private ISaveSystem saveSystem;

    /// <summary>
    /// Retrieves all recipes any user has created.
    /// </summary>
    public Recipe[] Recipes => recipes.ToArray();

    /// <summary>
    /// Retrieves all meals any user has created.
    /// </summary>
    public Meal[] Meals => meals.ToArray();
    
    /// <summary>
    /// Retrieves the shopping list controller.
    /// </summary>
    public ShoppingListController ShoppingListController => shoppingList;

    public FoodController(App app, ISaveSystem saveSystem)
    {
        this.app = app;
        this.saveSystem = saveSystem;
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
    /// Returns all recipes whose names contain the given search term.
    /// </summary>
    /// <param name="term">Search terms</param>
    /// <returns>The recipes that match</returns>
    public Recipe[] SearchRecipes(string term)
    {
        List<Recipe> matches = new List<Recipe>();

        foreach (Recipe recipe in recipes)
            if (recipe.Name.ToLower().Contains(term.ToLower().Trim()))
                matches.Add(recipe);

        return matches.ToArray();
    }

    /// <summary>
    /// Adds a meal with some pre-configured attributes to the user's saved meals.
    /// </summary>
    public void AddMeal(Meal meal){
        
        meals.Add(meal);
        Console.WriteLine(meal.ToString());
        foreach (Meal eachmeal in meals)
            Console.WriteLine(eachmeal.ToString());
    }

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
        foreach (Meal meal in meals){
            Console.WriteLine("Checking if " + meal.Name + " == " + name);
            if (meal.Name == name)
                return meal;
        }
        return null;
    }

    /// <summary>
    /// Returns all meals whose names contain the given search term.
    /// </summary>
    /// <param name="name">Search term</param>
    /// <returns>The meals that match</returns>
    public Meal[] SearchMeals(string name)
    {
        List<Meal> matches = new List<Meal>();
        
        foreach (Meal meal in meals)
            if (meal.Name.ToLower().Contains(name.ToLower().Trim()))
                matches.Add(meal);
        
        return matches.ToArray();
    }

    public Ingredient[] GetAllIngredients() => ingredientDatabase.GetAll();

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
    public IngredientStocks GetAllIngredientStocks(string username)
    {
        ingredientStocks.TryGetValue(username, out var stocks);
        return stocks;
    }

    /// <summary>
    /// Returns how much of an ingredient the given user has.
    /// </summary>
    public double GetSingleIngredientStock(string ingredientName, string username)
    {
        ingredientStocks.TryGetValue(username, out var stocks);
        var stock = stocks?.GetIngredientStock(ingredientName) ?? 0.0;
        return stock;
    }

    /// <summary>
    /// Checks if the given user has enough ingredients in stock for a given meal
    /// </summary>
    public bool EnoughIngredients(string mealName, string username)
    {
        ingredientStocks.TryGetValue(username, out var stocks);
        if (stocks == null) return false;
        
        Meal mealConsumed = GetMeal(mealName);

        // Check ingredient stock
        foreach (Ingredient ingredient in mealConsumed.Ingredients.Keys)
        {
            double requiredStock = mealConsumed.Ingredients[ingredient];

            if (stocks.GetIngredientStock(ingredient.Name) < requiredStock)
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

        // We dont have time to deal with this
        // if (!EnoughIngredients(mealName, username)) return false;

        // Meal consumed successfully
        MealConsumeEvent?.Invoke(mealConsumed, username);

        // Remove ingredient stock
        foreach (Ingredient ingredient in mealConsumed.Ingredients.Keys)
        {
            double requiredStock = mealConsumed.Ingredients[ingredient];
            EditIngredientStock(ingredient.Name, -requiredStock, username);
        }

        return true;
    }

    /// <summary>
    /// Edits the given user's stock of a certain ingredient, where change is positive
    /// if the user bought ingredients, and negative if ingredients were consumed.
    /// </summary>
    public void EditIngredientStock(string ingredientName, double change, string username)
    {
        if (!ingredientStocks.TryGetValue(username, out var stocks))
        {
            stocks = new IngredientStocks();
            ingredientStocks[username] = stocks;
        }
        
        var newStock = stocks.GetIngredientStock(ingredientName) + change;
        
        if (newStock <= 0)
            stocks.RemoveEntry(ingredientName);
        else
            stocks.SetEntry(ingredientName, newStock);
        
        shoppingList.Update(Recipes, username);
    }
    
    public void SaveUser(string folderName)
    {
        string username = SaveSystem.GetUsernameFromFile(folderName);
        saveSystem.GetFileSaver().Save(SaveSystem.GetFullPath(folderName,"food"), ingredientStocks[username].ToDictionary());
        ingredientStocks.Remove(username);
        shoppingList.RemoveUser(username);
    }

    public void LoadUser(string folderName)
    {
        string username = SaveSystem.GetUsernameFromFile(folderName);
        Dictionary<string, string> data = saveSystem.GetFileSaver().Load(SaveSystem.GetFullPath(folderName, "food"));
        IngredientStocks stock = new IngredientStocks();
        stock.FromDictionary(data);
        ingredientStocks[username] = stock;
    }

    public void SaveController()
    {
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

    public void LoadController()
    {
        if (!File.Exists(recipePath) || !File.Exists(mealPath))
            return;
        
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

    public void AddNewUser(User user)
    {
        ingredientStocks[user.UserName] = new IngredientStocks();
        shoppingList.AddUser(user.UserName);
        shoppingList.Update(Recipes, user.UserName);
    }
    
    public delegate void MealEventHandler(Meal meal, string username);
    public event MealEventHandler MealConsumeEvent;
}

public class IngredientStocks: ISaveObject
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

    public Dictionary<string, string> ToDictionary()
    {
        return stocks.ToDictionary(x => x.Key, x => x.Value.ToString());
    }

    public void FromDictionary(Dictionary<string, string> data)
    {
        stocks = data.ToDictionary(x => x.Key, x => double.Parse(x.Value));
    }

    public override bool Equals(object other)
    {
        IngredientStocks obj = other as IngredientStocks;
        return obj is not null && stocks.SequenceEqual(obj.stocks);
    }
}