using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace NutriApp.UI;

/// <summary>
/// Interface which defines a method handle() to handle menu interactions.
/// The 'actions' dictionary maps user menu options to corresponding invokers. 
/// Which is used to trigger specific commands.
/// </summary>
public interface Menu
{
    void Handle();
}

public interface Help
{
    public List<String> GetOptions();
}

/// <summary>
/// Allows you to pass actions(no return, no param functions) to be invoked like a command.
/// Used to swap menus
/// </summary>
public class ActionInvoker : Invoker
{
    private Action action;

    public ActionInvoker(Action action)
    {
        this.action = action;
    }
    public void Invoke()
    { 
        action?.Invoke();   
    }
}

class FitnessMenu : Menu, Help
{
    private Dictionary<string, Invoker> actions;
    private UIController uIController;


    public FitnessMenu(UIController uIController)
    {
        this.uIController = uIController;
        var addWorkout = new AddWorkoutCommand(uIController.app);
        var setFitness = new SetFitnessGoalCommand(uIController.app);
        var setWeight = new SetWeightGoalCommand(uIController.app);
        var viewTargetCalories = new ViewTargetCaloriesCommand(uIController.app);
        
        actions = new Dictionary<string, Invoker>
        {
            { "Add Workout", new PTAddWorkoutInvoker(addWorkout) },
            { "Set Fitness Goal", new PTSetFitnessGoalInvoker(setFitness) },
            { "Set Weight Goal", new PTSetWeightGoalInvoker(setWeight, uIController.app)},
            { "View Target Calories", new PTViewTargetCaloriesInvoker(viewTargetCalories) },
            {"Main Menu", new ActionInvoker(() => uIController.menu = new MainMenu(uIController))},
            {"Help", new PTHelpInvoker(this)}
        };

        new PTAddWorkoutUpdater(addWorkout, uIController.app);
        new PTSetFitnessGoalUpdater(setFitness, uIController.app);
        new PTSetWeightGoalInvoker(setWeight, uIController.app);
        new PTViewTargetCaloriesUpdater(viewTargetCalories, uIController.app);
    }

    public void Handle()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("welcome to the fitness menu");
        while (true)
        {
            Console.WriteLine("please enter a Command (Help to get options)");
            string input = Console.ReadLine();
            if (!actions.ContainsKey(input))
            {
                Console.WriteLine(input + " is not a valid input");
            }
            else
            {
                actions[input].Invoke();
            }
        }       
    }

    /// <summary>
    /// Prompts the weight of user at the end of the day.
    /// </summary>
    /// <param name="datetime"></param>
    public void PromptWeight(DateTime datetime)
    {
        
    }

    public List<string> GetOptions()
    {
        return actions.Keys.ToList();
    }
}


class FoodMenu : Menu, Help
{
    private Dictionary<string, Invoker> actions;
    private UIController uIController;

    public FoodMenu(UIController uIController)
    {
        this.uIController = uIController;
        var consumeMeal = new ConsumeMealCommand(uIController.app);
        var createRecipe = new CreateRecipesCommand(uIController.app);
        var getShoppingList = new GetShoppingListCommand(uIController.app);
        var purchaseFood = new PurchaseFoodCommand(uIController.app);
        var searchIngredients = new SearchingIngredientsCommand(uIController.app);
        
        actions = new Dictionary<string, Invoker>
            {
                { "Consume Meal", new PTConsumeMealInvoker(consumeMeal)},
                { "Create Recipe", new PTCreateRecipesInvoker(createRecipe)},
                { "Get Shopping List", new PTGetShoppingListInvoker(getShoppingList)},
                { "Purchase Food", new PTPurchaseFoodInvoker(purchaseFood)},
                { "Search Ingredients", new PTSearchIngredientsInvoker(searchIngredients)},
                {"Main Menu", new ActionInvoker(() => uIController.menu = new MainMenu(uIController))},
                {"Help", new PTHelpInvoker(this)}
            };
        
        new PTConsumeMealUpdater(consumeMeal, uIController.app);
        new PTCreateRecipesUpdater(createRecipe, uIController.app);
        new PTGetShoppingListUpdater(getShoppingList, uIController.app);
        new PTPurchaseFoodUpdater(purchaseFood, uIController.app);
        new PTSearchIngredientsUpdater(searchIngredients, uIController.app);

    }

    /// <summary>
    /// Sets the current menu to the food menu.
    /// </summary>
    public void Handle()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("welcome to the food menu");
        while (true)
        {
            Console.WriteLine("please enter a Command (Help to get options)");
            string input = Console.ReadLine();
            if (!actions.ContainsKey(input))
            {
                Console.WriteLine(input + " is not a valid input");
            }
            else
            {
                actions[input].Invoke();
            }
        }   
    }


    public List<string> GetOptions()
    {
        return actions.Keys.ToList();
    }
}

class HistoryMenu : Menu, Help
{
    private Dictionary<string, Invoker> actions;
    private UIController uIController;

    public HistoryMenu(UIController uIController)
    {
        this.uIController = uIController;
        var viewCalories = new ViewCaloriesCommand(uIController.app);
        var viewMeals = new ViewMealsCommand(uIController.app);
        var viewWeight = new ViewWeightCommand(uIController.app);
        var viewWorkouts = new ViewWorkoutsCommand(uIController.app);

            actions = new Dictionary<string, Invoker>
            {
                {"View Calories", new PTViewCaloriesInvoker(viewCalories)},
                {"View Meals", new PTViewMealsInvoker(viewMeals)},
                {"View Weight", new PTViewWeightInvoker(viewWeight)},
                {"View Workouts", new PTViewWorkoutsInvoker(viewWorkouts)},
                {"Main Menu", new ActionInvoker(() => uIController.menu = new MainMenu(uIController))},
                {"Help", new PTHelpInvoker(this)}
            };
        
        new PTViewCaloriesUpdater(viewCalories, uIController.app);
        new PTViewMealsUpdater(viewMeals, uIController.app);
        new PTViewWeightUpdater(viewWeight, uIController.app);
        new PTViewWorkoutsUpdater(viewWorkouts, uIController.app);

    }
    

    /// <summary>
    /// Sets the current menu to the history menu.
    /// </summary>
    public void Handle()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("welcome to the history menu");
        while (true)
        {
            Console.WriteLine("please enter a Command (Help to get options)");
            string input = Console.ReadLine();
            if (!actions.ContainsKey(input))
            {
                Console.WriteLine(input + " is not a valid input");
            }
            else
            {
                actions[input].Invoke();
            }
        }    
    }

    public List<string> GetOptions()
    {
        return actions.Keys.ToList();
    }
}


class ProfileMenu : Menu, Help
{
    private Dictionary<string, Invoker> actions;
    private UIController uIController;

    public ProfileMenu(UIController uIController)
    {
        this.uIController = uIController;
        var clearHistory = new ClearHistoryCommand(uIController.app);
        var setDayLength = new SetDayLengthCommand(uIController.app);
        var quit = new QuitCommand(uIController.app);
        actions = new Dictionary<string, Invoker>
            {
                {"Clear History", new PTClearHistoryInvoker(clearHistory)},
                {"Set Day Length", new PTSetDayLengthInvoker(setDayLength)},
                {"Quit", new PTQuitInvoker(quit)},
                {"Main Menu", new ActionInvoker(() => uIController.menu = new MainMenu(uIController))},
                {"Help", new PTHelpInvoker(this)}
            };
        new PTClearHistoryUpdater(clearHistory, uIController.app);
        new PTSetDayLengthUpdater(setDayLength, uIController.app);
        //no updater for quit
    }

    /// <summary>
    /// Sets the current menu to the profile menu.
    /// </summary>

    public void Handle()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("welcome to the profile menu");
        while (true)
        {
            Console.WriteLine("please enter a Command (Help to get options)");
            string input = Console.ReadLine();
            if (!actions.ContainsKey(input))
            {
                Console.WriteLine(input + " is not a valid input");
            }
            else
            {
                actions[input].Invoke();
            }
        }       }

    public List<string> GetOptions()
    {
        return actions.Keys.ToList();
    }
}

    

class MainMenu : Menu
{
    private UIController uIController;
    
    private Dictionary<string, Menu> _menus;

    public MainMenu(UIController uIController)
    {
        this.uIController = uIController;
        
        _menus = new Dictionary<string, Menu>()
        {
            {"fitness", new FitnessMenu(uIController) },
            {"food", new FoodMenu(uIController)},
            {"history", new HistoryMenu(uIController)},
            {"profile", new ProfileMenu(uIController)},
        };
        
        Handle();
    }

    /// <summary>
    /// Sets the current menu to the main menu.
    /// </summary>
    public void Handle()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("welcome to the main menu");
        while (true)
        {
            Console.WriteLine("please enter a menu to navigate to(fitness, food, history, profile)");
            string input = Console.ReadLine();
            if (!_menus.ContainsKey(input))
            {
                Console.WriteLine(input + " is not a valid input");
            }
            else
            {
                uIController.menu = _menus[input];
            }
        }
    }
}