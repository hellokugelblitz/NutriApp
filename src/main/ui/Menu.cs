using System;
using System.Collections.Generic;

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

class FitnessMenu : Menu
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
            { "View Target Calories", new PTViewTargetCaloriesInvoker(new ViewTargetCaloriesCommand(uIController.app)) }
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
    public void PromptWeight(DateTime datetime) { }
}


class FoodMenu : Menu
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
                { "Search Ingredients", new PTSearchIngredientsInvoker(new SearchingIngredientsCommand(uIController.app))}
            };
    }

    /// <summary>
    /// Sets the current menu to the food menu.
    /// </summary>
    public void Handle()
    {
        uIController.menu = this;

    }


}

class HistoryMenu : Menu
{
    private Dictionary<string, Invoker> actions;
    private UIController uIController;

    public HistoryMenu(UIController uIController)
    {
        this.uIController = uIController;
        actions = new Dictionary<string, Invoker>
            {
                { "View Calories", new PTViewCaloriesInvoker(new ViewCaloriesCommand(uIController.app))},
                { "View Meals", new PTViewMealsInvoker(new ViewMealsCommand(uIController.app))},
                { "View Weight", new PTViewWeightInvoker(new ViewWeightCommand(uIController.app))},
                { "View Workouts", new PTViewWorkoutsInvoker(new ViewWorkoutsCommand(uIController.app))}
            };
        
    }
    

    /// <summary>
    /// Sets the current menu to the history menu.
    /// </summary>
    public void Handle()
    {
        uIController.menu = this;
    }
}


class ProfileMenu : Menu
{
    private Dictionary<string, Invoker> actions;
    private UIController uIController;

    public ProfileMenu(UIController uIController)
    {
        this.uIController = uIController;
        actions = new Dictionary<string, Invoker>
            {
                { "Clear History", new PTClearHistoryInvoker(new ClearHistoryCommand(uIController.app))},
                { "Set Day Length", new PTSetDayLengthInvoker(new SetDayLengthCommand(uIController.app))},
                {"Quit", new PTQuitInvoker(new QuitCommand(uIController.app))}
            };

    }

    /// <summary>
    /// Sets the current menu to the profile menu.
    /// </summary>

    public void Handle()
    {
        uIController.menu = this;
    }
}

    

class MainMenu : Menu
{
    private UIController uIController;

    public MainMenu(UIController uIController)
    {
        this.uIController = uIController;
    }

    /// <summary>
    /// Sets the current menu to the main menu.
    /// </summary>
    public void Handle()
    {
        uIController.menu = this;
    }
}