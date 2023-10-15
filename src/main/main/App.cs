using System;
using NutriApp.Food;

namespace NutriApp
{
    public class App
    {
        // private HistoryController history;
        // private GoalController goal;
        private FoodController food;
        private DateTime date;

        // public HistoryController HistoryControl;
        // public GoalController GoalControl;
        public FoodController FoodControl { get; }
        public DateTime TimeStamp { get; }
        public int DailyCalories { get; }

        public App() {}

        public delegate void DayEventHandler(DateTime date);
        public event DayEventHandler DayEndEvent;

        public void SubscribeDayEndEvent(DayEventHandler dayEndEvent) {}
    }
}