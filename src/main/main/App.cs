using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using NutriApp.Food;
using NutriApp.History;
using NutriApp.Goal;
using NutriApp.UI;
using NutriApp.Workout;

namespace NutriApp;

public class App
{
    private HistoryController history;
    private GoalController goal;
    private WorkoutController workout;
    private FoodController food;
    private UIController ui;
    private DateTime date;
    private User user;
    private double dayLength;
    private Task<None> timerThread;
    
    public HistoryController HistoryControl => history;
    public GoalController GoalControl => goal; 
    public WorkoutController WorkoutControl => workout;
    public FoodController FoodControl => food;
    public UIController UIControl => ui;
    public DateTime TimeStamp => date;
    
    public User User { get => user; set => user = value; }
    public double DayLength { set => dayLength = value; }

    public App(double dayLength)
    {
        this.dayLength = dayLength;
        date = DateTime.Now;
        timerThread = new Task<None>(DayLoop);
        timerThread.Start();

        goal = new GoalController(this);
        workout = new WorkoutController();
        food = new FoodController(this);
        history = new HistoryController(this);
        ui = new UIController(this);
    }

    public void KillTimer()
    {
        timerThread.Dispose();
    }

    public List<Workout.Workout> GetRecommendedWorkouts() => workout.GenerateRecommendedWorkouts(history.Workouts);
    public double GetTodaysCalories() { return -1d; }

    public delegate void DayEventHandler(DateTime date);
    public event DayEventHandler DayEndEvent;

    public void SubscribeDayEndEvent(DayEventHandler dayEndEvent)
    {
        DayEndEvent += dayEndEvent;
    }

    public static void Main(string[] args)
    {
        App app = new App(1);
    }

    private None DayLoop()
    {
        while (true)
        {
            Thread.Sleep((int)(1000 * 60 * dayLength));
            DayEndEvent?.Invoke(TimeStamp);
            Console.WriteLine("new day " + TimeStamp);
            date = date.AddDays(1d);
        }
    }
}
