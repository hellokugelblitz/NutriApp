using System;
using System.Collections.Generic;

namespace NutriApp
{
    interface Menu
    {
        void Handle();
    }

    class Fitness<T> : Menu
    {
        private Dictionary<string, CommandInvoker<T>> actions;
        private UIController uIController;

        public Fitness(UIController uIController)
        {
            this.uIController = uIController;
            actions = new Dictionary<string, CommandInvoker<T>>
            {
                { "Add Workout", new PTAddWorkoutInvoker(new AddWorkoutCommand(uIController.app), uIController.app) },
                { "Set Fitness Goal", new PTSetFitnessGoalInvoker(new SetFitnessGoalCommand(uIController.app)) },
                { "Set Weight Goal", new PTSetWeightGoalInvoker(new SetWeightGoalCommand(uIController.app)) },
                { "View Target Calories", new PTViewCaloriesInvoker(new ViewCaloriesCommand(uIController.app)) }
            };
        }

        public void PromptWeight(DateTime datetime)
        {
            // uses dependency arrow to PTSetWeightInvoker
            actions["Set Weight Goal"].Invoke();
        }

        public void Handle()
        {
            uIController.menu = new Fitness(uIController);
        }
    }

    class FoodMenu : Menu
    {
        private Dictionary<string, CommandInvoker> actions;
        private UIController uIController;

        public FoodMenu(UIController uIController)
        {
            this.uIController = uIController;
            actions = new Dictionary<string, CommandInvoker>
            {
                { "Consume Meal", new PTConsumeMealInvoker(new ConsumeMealCommand(uIController.app)) },
                { "Create Recipe", new PTCreateRecipeInvoker(new CreateRecipeCommand(uIController.app)) },
                { "Get Shopping List", new PTGetShoppingListInvoker(new GetShoppingListCommand(uIController.app)) },
                { "Purchase Food", new PTPurchaseFoodInvoker(new PurchaseFoodCommand(uIController.app)) },
                { "Search Ingredients", new PTSearchingIngredientsInvoker(new SearchingIngredientsCommand(uIController.app)) }
            };
        }

        public void Handle()
        {
            uIController.menu = new FoodMenu(uIController);
        }
    }

    class HistoryMenu : Menu
    {
        private Dictionary<string, CommandInvoker> actions;
        private UIController uIController;

        public HistoryMenu(UIController uIController)
        {
            this.uIController = uIController;
            actions = new Dictionary<string, CommandInvoker>
            {
                { "View Calories", new PTViewCaloriesInvoker(new ViewCaloriesCommand(uIController.app)) },
                { "View Meals", new PTViewMealsInvoker(new ViewMealsCommand(uIController.app)) },
                { "View Weight", new PTViewWeightInvoker(new ViewWeightCommand(uIController.app)) },
                { "View Workouts", new PTViewWorkoutsInvoker(new ViewWorkoutsCommand(uIController.app)) }
            };
        }

        public void Handle()
        {
            uIController.menu = new HistoryMenu(uIController);
        }
    }

    class ProfileMenu : Menu
    {
        private Dictionary<string, CommandInvoker> actions;
        private UIController uIController;

        public ProfileMenu(UIController uIController)
        {
            this.uIController = uIController;
            actions = new Dictionary<string, CommandInvoker>
            {
                { "Clear History", new PTClearHistoryInvoker(new ClearHistoryCommand(uIController.app)) },
                { "Set Day Length", new PTSetDayLengthInvoker(new SetDayLengthCommand(uIController.app)) }
            };
        }

        public void Handle()
        {
            uIController.menu = new ProfileMenu(uIController);
        }
    }

    class MainMenu : Menu
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