using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using NutriApp.Food;
using NutriApp.History;
using NutriApp.Goal;
using NutriApp.UI;
using NutriApp.Workout;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NutriApp.Controllers.Middleware;
using NutriApp.Notifications;
using NutriApp.Save;

namespace NutriApp;

public class App
{
    private readonly string userPath = $"{Persistence.UserDataPath}\\user.json";
    private readonly string datePath = $"{Persistence.DateDataPath}\\date.json";

    private ISaveSystem saveSystem;
    private UserController user;
    private HistoryController history;
    private GoalController goal;
    private WorkoutController workout;
    private FoodController food;
    private UserController userCtrl;
    private DateTime date;
    private double dayLength;
    private Task<None> timerThread;

    public ISaveSystem SaveSyst => saveSystem;
    public UserController UserControl => user;
    public HistoryController HistoryControl => history;
    public GoalController GoalControl => goal; 
    public WorkoutController WorkoutControl => workout;
    public FoodController FoodControl => food;
    public User User { get; set; }
    public UserController UserControl => userCtrl;
    public DateTime TimeStamp => date;
    
    public double DayLength { set => dayLength = value; }

    public App(double dayLength)
    {
        this.dayLength = dayLength;
        date = DateTime.Now;
        timerThread = new Task<None>(DayLoop);
        timerThread.Start();

        saveSystem = new SaveSystem();
        user = new UserController(saveSystem);
        workout = new WorkoutController();
        food = new FoodController(this);
        history = new HistoryController(this);
        goal = new GoalController(this);
        userCtrl = new UserController(new SaveSystem());
        NotificationController.Instance.AppInstance = this;

        food.MealConsumeEvent += goal.ConsumeMealHandler;
        food.MealConsumeEvent += history.AddMeal;
    }

    public void KillTimer()
    {
        timerThread.Dispose();
    }

    public List<Workout.Workout> GetRecommendedWorkouts(string username) 
        => workout.GenerateRecommendedWorkouts(history.GetWorkouts(username));
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

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton(_ => new App(1));

        builder.Services.AddAuthentication("NutriAppScheme")
            .AddScheme<AuthenticationSchemeOptions, NutriAppAuthHandler>("NutriAppScheme", _ => { });
        builder.Services.AddAuthorization();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "NutriApp API", Version = "v1" });
            c.OperationFilter<AddHeaderOperationFilter>();
        });

        var webapp = builder.Build();

        if (webapp.Environment.IsDevelopment())
        {
            webapp.UseSwagger();
            webapp.UseSwaggerUI();
        }
        
        webapp.MapControllers();
        webapp.Run();
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
    
    // public void Save()
    // {
    //     // Write the user to a JSON file for persistence
    //     var userJson = JsonConvert.SerializeObject(user);
    //     File.WriteAllText(userPath, userJson);
    //     
    //     // Write the current date to a JSON file for persistence
    //     var timeJson = JsonConvert.SerializeObject(new { date });
    //     File.WriteAllText(datePath, timeJson);
    // }
    //
    // public void Load()
    // {
    //     // Don't do anything if data files don't exist yet (e.g. first startup)
    //     if (!File.Exists(userPath) || !File.Exists(datePath))
    //         return;
    //
    //     // Read the user from a JSON file
    //     var json = File.ReadAllText(userPath);
    //     user = JsonConvert.DeserializeObject<User>(json);
    //     
    //     // Read the date from a JSON file
    //     json = File.ReadAllText(datePath);
    //     date = JsonConvert.DeserializeObject<DateTime>(json);
    // }
