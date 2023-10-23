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
        actions = new Dictionary<string, Invoker>
        {
            { "Add Workout", new PTAddWorkoutInvoker(new AddWorkoutCommand(uIController.app)) },
            { "Set Fitness Goal", new PTSetFitnessGoalInvoker(new SetFitnessGoalCommand(uIController.app)) },
            {
                "Set Weight Goal",
                new PTSetWeightGoalInvoker(new SetWeightGoalCommand(uIController.app), uIController.app)
            },
            { "View Target Calories", new PTViewTargetCaloriesInvoker(new ViewTargetCaloriesCommand(uIController.app)) },
            {"Main Menu", new ActionInvoker(() => uIController.menu = new MainMenu(uIController))},
            {"Help", new PTHelpInvoker(this)}
        };
    }

    public void Handle()
    {
        uIController.menu = this;
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
        actions = new Dictionary<string, Invoker>
            {
                { "Consume Meal", new PTConsumeMealInvoker(new ConsumeMealCommand(uIController.app))},
                { "Create Recipe", new PTCreateRecipesInvoker(new CreateRecipesCommand(uIController.app))},
                { "Get Shopping List", new PTGetShoppingListInvoker(new GetShoppingListCommand(uIController.app))},
                { "Purchase Food", new PTPurchaseFoodInvoker(new PurchaseFoodCommand(uIController.app))},
                { "Search Ingredients", new PTSearchIngredientsInvoker(new SearchingIngredientsCommand(uIController.app))},
                {"Main Menu", new ActionInvoker(() => uIController.menu = new MainMenu(uIController))},
                {"Help", new PTHelpInvoker(this)}
            };
    }

    /// <summary>
    /// Sets the current menu to the food menu.
    /// </summary>
    public void Handle()
    {
        uIController.menu = this;

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
        actions = new Dictionary<string, Invoker>
            {
                {"View Calories", new PTViewCaloriesInvoker(new ViewCaloriesCommand(uIController.app))},
                {"View Meals", new PTViewMealsInvoker(new ViewMealsCommand(uIController.app))},
                {"View Weight", new PTViewWeightInvoker(new ViewWeightCommand(uIController.app))},
                {"View Workouts", new PTViewWorkoutsInvoker(new ViewWorkoutsCommand(uIController.app))},
                {"Main Menu", new ActionInvoker(() => uIController.menu = new MainMenu(uIController))},
                {"Help", new PTHelpInvoker(this)}
            };
        
    }
    

    /// <summary>
    /// Sets the current menu to the history menu.
    /// </summary>
    public void Handle()
    {
        uIController.menu = this;
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
        actions = new Dictionary<string, Invoker>
            {
                {"Clear History", new PTClearHistoryInvoker(new ClearHistoryCommand(uIController.app))},
                {"Set Day Length", new PTSetDayLengthInvoker(new SetDayLengthCommand(uIController.app))},
                {"Quit", new PTQuitInvoker(new QuitCommand(uIController.app))},
                {"Main Menu", new ActionInvoker(() => uIController.menu = new MainMenu(uIController))},
                {"Help", new PTHelpInvoker(this)}
            };

    }

    /// <summary>
    /// Sets the current menu to the profile menu.
    /// </summary>

    public void Handle()
    {
        uIController.menu = this;
    }

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