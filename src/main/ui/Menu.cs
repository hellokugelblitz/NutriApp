using System;
using System.Collections.Generic;

namespace NutriApp.UI
{
    interface Menu
    {
        void Handle();
    }

<<<<<<< HEAD
    class Fitness<T> : Menu
=======
    class FitnessMenu : Menu
>>>>>>> main
    {
        private Dictionary<string, CommandInvoker<T>> actions;
        private UIController uIController;

        public FitnessMenu(UIController uIController)
        {
            this.uIController = uIController;
<<<<<<< HEAD
            actions = new Dictionary<string, CommandInvoker<T>>
            {
                { "Add Workout", new PTAddWorkoutInvoker(new AddWorkoutCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "Set Fitness Goal", new PTSetFitnessGoalInvoker(new SetFitnessGoalCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "Set Weight Goal", new PTSetWeightInvoker(new SetWeightCommand(uIController.app), uIController.app) as CommandInvoker<T> },
                { "View Target Calories", new PTViewTargetCaloriesInvoker(new ViewTargetCaloriesCommand(uIController.app), uIController.app) as CommandInvoker<T> }
            };
=======
>>>>>>> main
        }

        public void PromptWeight(DateTime datetime)
        {
<<<<<<< HEAD
            actions["Set Weight Goal"].Invoke();
=======
            // uses dependency arrow to PTSetWeightInvoker
>>>>>>> main
        }

        public void Handle()
        {
<<<<<<< HEAD
            uIController.menu = new Fitness<T>(uIController);
=======
            uIController.SetMenu(new FitnessMenu(uIController));
>>>>>>> main
        }
    }

    class FoodMenu : Menu
    {
        private Dictionary<string, CommandInvoker> actions;
        private UIController uIController;

        public FoodMenu(UIController uIController)
        {
            this.uIController = uIController;
        }

        public void Handle()
        {
            uIController.SetMenu(new FoodMenu(uIController));
        }
    }

    class HistoryMenu : Menu
    {
        private Dictionary<string, CommandInvoker> actions;
        private UIController uIController;

        public HistoryMenu(UIController uIController)
        {
            this.uIController = uIController;
        }

        public void Handle()
        {
            uIController.SetMenu(new HistoryMenu(uIController));
        }
    }

    class ProfileMenu : Menu
    {
        private Dictionary<string, CommandInvoker> actions;
        private UIController uIController;

        public ProfileMenu(UIController uIController)
        {
            this.uIController = uIController;
        }

        public void Handle()
        {
            uIController.SetMenu(new ProfileMenu(uIController));
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