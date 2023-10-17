using System;
using System.Collections.Generic;

namespace NutriApp.UI
{
    interface Menu
    {
        void Handle();
    }

    class FitnessMenu<T> : Menu
    {
        private Dictionary<string, CommandInvoker<T>> actions;
        private UIController uIController;

        public FitnessMenu(UIController uIController)
        {
            this.uIController = uIController;
            actions = new Dictionary<string, CommandInvoker<T>>
            {
                { "Add Workout", new PTAddWorkoutInvoker(new AddWorkoutCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "Set Fitness Goal", new PTSetFitnessGoalInvoker(new SetFitnessGoalCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "Set Weight Goal", new PTSetWeightGoalInvoker(new SetWeightGoalCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "View Target Calories", new PTViewTargetCaloriesInvoker(new ViewTargetCaloriesCommand(uIController.app), uIController.app) as CommandInvoker<T> }
            };
        }

        public void PromptWeight(DateTime datetime)
        {
            actions["Set Weight Goal"].Invoke();
        }

        public void Handle()
        {
            uIController.menu = new FitnessMenu<T>(uIController);
        }
    }

    class FoodMenu<T> : Menu
    {
        private Dictionary<string, CommandInvoker<T>> actions;
        private UIController uIController;

        public FoodMenu(UIController uIController)
        {
            this.uIController = uIController;
            actions = new Dictionary<string, CommandInvoker<T>>
            {
                { "Consume Meal", new PTConsumeMealInvoker(new ConsumeMealCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "Create Recipe", new PTCreateRecipesInvoker(new CreateRecipesCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "Get Shopping List", new PTGetShoppingListInvoker(new GetShoppingListCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "Purchase Food", new PTPurchaseFoodInvoker(new PurchaseFoodCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "Search Ingredients", new PTSearchIngredientsInvoker(new SearchingIngredientsCommand(uIController.app), uIController.app) as CommandInvoker<T> }
            };
        }

        public void Handle()
        {
            uIController.menu = new FoodMenu<T>(uIController);
        }
    }

    class HistoryMenu<T> : Menu
    {
        private Dictionary<string, CommandInvoker<T>> actions;
        private UIController uIController;

        public HistoryMenu(UIController uIController)
        {
            this.uIController = uIController;
            actions = new Dictionary<string, CommandInvoker<T>>
            {
                { "View Calories", new PTViewCaloriesInvoker(new ViewCaloriesCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "View Meals", new PTViewMealsInvoker(new ViewMealsCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "View Weight", new PTViewWeightInvoker(new ViewWeightCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "View Workouts", new PTViewWorkoutsInvoker(new ViewWorkoutsCommand(uIController.app), uIController.app) as CommandInvoker<T> }
            };
        }

        public void Handle()
        {
            uIController.menu = new HistoryMenu<T>(uIController);
        }
    }

    class ProfileMenu<T> : Menu
    {
        private Dictionary<string, CommandInvoker<T>> actions;
        private UIController uIController;

        public ProfileMenu(UIController uIController)
        {
            this.uIController = uIController;
            actions = new Dictionary<string, CommandInvoker<T>>
            {
                { "Clear History", new PTClearHistoryInvoker(new ClearHistoryCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "Set Day Length", new PTSetDayLengthInvoker(new SetDayLengthCommand(uIController.app), uIController.app) as CommandInvoker<T> }
            };
        }

        public void Handle()
        {
            uIController.menu = new ProfileMenu<T>(uIController);
        }
    }

    class MainMenu : Menu
    {
        private Dictionary<string, CommandInvoker> actions;
        private UIController uIController;

        public MainMenu(UIController uIController)
        {
            this.uIController = uIController;
        }

        public void Handle()
        {
            uIController.SetMenu(new MainMenu(uIController));
        }
    }
}