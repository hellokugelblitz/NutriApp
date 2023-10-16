using System;
using NutriApp.Food;
using NutriApp.History;
using NutriApp.Goal;
using NutriApp.Workout;

namespace NutriApp
{
    public class App
    {
        private HistoryController history;
        private GoalController goal;
        private WorkoutController workout;
        private FoodController food;
        private DateTime date;
        private User user;
        public HistoryController HistoryControl;
        public GoalController GoalControl { get => goal; }
        public WorkoutController WorkoutControl { get => workout; }
        public FoodController FoodControl { get; }
        public DateTime TimeStamp { get => DateTime.Now; }
        public int DailyCalories { get; }
        public User User { get => user; set => user = value; }

        public App() {}

        public FoodController GetFoodController() {
            return food;}
        public delegate void DayEventHandler(DateTime date);
        public event DayEventHandler DayEndEvent;

        public void SubscribeDayEndEvent(DayEventHandler dayEndEvent) {}
    }
}