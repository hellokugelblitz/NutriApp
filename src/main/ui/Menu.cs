using System;

namespace NutriApp
{
    interface Menu
    {
        void Handle();
    }

    class Fitness : Menu
    {
        private Dictionary<string, CommandInvoker> actions;
        private UIController uIController;

        public Fitness(UIController uIController)
        {
            this.uIController = uIController;
        }

        public void PromptWeight(DateTime datetime)
        {
            // uses dependency arrow to PTSetWeightInvoker
        }

        public void Handle()
        {
            uIController.SetMenu(new Fitness(uIController));
        }
    }

    class Food : Menu
    {
        private Dictionary<string, CommandInvoker> actions;
        private UIController uIController;

        public Food(UIController uIController)
        {
            this.uIController = uIController;
        }

        public void Handle()
        {
            uIController.SetMenu(new Food(uIController));
        }
    }

    class History : Menu
    {
        private Dictionary<string, CommandInvoker> actions;
        private UIController uIController;

        public History(UIController uIController)
        {
            this.uIController = uIController;
        }

        public void Handle()
        {
            uIController.SetMenu(new History(uIController));
        }
    }

    class Profile : Menu
    {
        private Dictionary<string, CommandInvoker> actions;
        private UIController uIController;

        public Profile(UIController uIController)
        {
            this.uIController = uIController;
        }

        public void Handle()
        {
            uIController.SetMenu(new Profile(uIController));
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