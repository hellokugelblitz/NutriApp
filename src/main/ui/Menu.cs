using System;
using System.Collections.Generic;

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
        }

        public void Handle()
        {
            uIController.menu = new ProfileMenu(uIController);
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
            uIController.menu = new MainMenu(uIController);
        }
    }
}