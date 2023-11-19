using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NutriApp.Food;
using NutriApp.History;
using NutriApp.Goal;
using NutriApp.UI;
using NutriApp.Workout;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NutriApp;

public class App
{
    private readonly string userPath = $"{Persistence.UserDataPath}\\user.json";
    private readonly string datePath = $"{Persistence.DateDataPath}\\date.json";

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

        workout = new WorkoutController();
        food = new FoodController(this);
        history = new HistoryController(this);
        goal = new GoalController(this);

        food.MealConsumeEvent += goal.ConsumeMealHandler;
        food.MealConsumeEvent += history.AddMeal;
        
        ui = new UIController(this);
    }

    public void KillTimer()
    {
        timerThread.Dispose();
    }

    public List<Workout.Workout> GetRecommendedWorkouts() 
        => workout.GenerateRecommendedWorkouts(history.Workouts);
    public double GetTodaysCalories() { return -1d; }

    public delegate void DayEventHandler(DateTime date);
    public event DayEventHandler DayEndEvent;

    public void SubscribeDayEndEvent(DayEventHandler dayEndEvent)
    {
        DayEndEvent += dayEndEvent;
    }

    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        WebApplication webapp = builder.Build();
        webapp.MapControllers();
        webapp.Run();

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

    public void Save()
    {
        // Write the user to a JSON file for persistence
        var userJson = JsonConvert.SerializeObject(user);
        File.WriteAllText(userPath, userJson);
        
        // Write the current date to a JSON file for persistence
        var timeJson = JsonConvert.SerializeObject(new { date });
        File.WriteAllText(datePath, timeJson);
    }

    public void Load()
    {
        // Don't do anything if data files don't exist yet (e.g. first startup)
        if (!File.Exists(userPath) || !File.Exists(datePath))
            return;

        // Read the user from a JSON file
        var json = File.ReadAllText(userPath);
        user = JsonConvert.DeserializeObject<User>(json);
        
        // Read the date from a JSON file
        json = File.ReadAllText(datePath);
        date = JsonConvert.DeserializeObject<DateTime>(json);
    }
}
