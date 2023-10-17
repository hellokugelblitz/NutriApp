using System;
using System.Threading;
using System.Collections.Generic;
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
        private double dayLength;
        
        public HistoryController HistoryControl;
        public GoalController GoalControl { get => goal; }
        public WorkoutController WorkoutControl { get => workout; }
        public FoodController FoodControl { get; }
        public DateTime TimeStamp { get => DateTime.Now; }
        public int DailyCalories { get; }
        public User User { get => user; set => user = value; }
        public double DayLength { set => dayLength = value; }

        public App(double dayLength)
        {
            this.dayLength = dayLength;
            date = DateTime.Now;
            Console.WriteLine("day length " + this.dayLength);
            Thread timer = new Thread(DayLoop);
            timer.Start();
        }

        public FoodController GetFoodController() {
            return food;}

        public List<Workout.Workout> GetRecommendedWorkouts() => workout.GenerateRecommendedWorkouts();

        public delegate void DayEventHandler(DateTime date);
        public event DayEventHandler DayEndEvent;

        public void SubscribeDayEndEvent(DayEventHandler dayEndEvent)
        {
            DayEndEvent += dayEndEvent;
        }

        public static void Main(string[] args)
        {
            App app = new App(2);
        }

        private void DayLoop()
        {
            while (true)
            {
                Thread.Sleep((int)(1000 * 60 * dayLength));
                DayEndEvent?.Invoke(TimeStamp);
                date = date.AddDays(1d);
            }
        }
    }
}