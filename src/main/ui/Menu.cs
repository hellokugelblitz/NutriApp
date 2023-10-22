using System;
using System.Collections.Generic;

namespace NutriApp.UI
{
    public interface Menu
    {
        void Handle();
    }

    public class FitnessMenu : Menu
    {
        private Dictionary<string, Invoker> actions;
        private UIController uIController;

        public FitnessMenu(UIController uIController)
        {
            this.uIController = uIController;
            actions = new Dictionary<string, Invoker>
            {
                { "Add Workout", new PTAddWorkoutInvoker(new AddWorkoutCommand(uIController.app))},
                { "Set Fitness Goal", new PTSetFitnessGoalInvoker(new SetFitnessGoalCommand(uIController.app))},
                { "Set Weight Goal", new PTSetWeightGoalInvoker(new SetWeightGoalCommand(uIController.app), uIController.app)},
                { "View Target Calories", new PTViewTargetCaloriesInvoker(new ViewTargetCaloriesCommand(uIController.app))}
            };
        }

        public void PromptWeight(DateTime datetime)
        {
            actions["Set Weight Goal"].Invoke();
        }

        public void Handle()
        {
            uIController.menu = new FitnessMenu(uIController);
        }
    }

    public class FoodMenu : Menu
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

        public void Handle()
        {
            uIController.menu = new FoodMenu(uIController);
        }
    }

    public class HistoryMenu : Menu
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

        public void Handle()
        {
            uIController.menu = new HistoryMenu(uIController);
        }
    }

    public class ProfileMenu : Menu
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

        public void Handle()
        {
            uIController.menu = new ProfileMenu(uIController);
        }
    }

    public class MainMenu : Menu
    {
        private UIController uIController;

        public MainMenu(UIController uIController)
        {
            this.uIController = uIController;
        }

        public void Handle()
        {
            uIController.menu = new MainMenu(uIController);
        }
    }
}