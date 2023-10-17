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
        
        public HistoryController HistoryControl => history;
        public GoalController GoalControl { get => goal; }
        public WorkoutController WorkoutControl { get => workout; }
        public FoodController FoodControl { get; }
        public DateTime TimeStamp { get => DateTime.Now; }
        public double DailyCaloriesConsumed { get; }
        public User User { get => user; set => user = value; }

        public App() {}

        public FoodController GetFoodController() {
            return food;}
        public List<Workout.Workout> GetRecommendedWorkouts() => workout.GenerateRecommendedWorkouts();
        public delegate void DayEventHandler(DateTime date);
        public event DayEventHandler DayEndEvent;

        public void SubscribeDayEndEvent(DayEventHandler dayEndEvent) {}
    }
}