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
using NutriApp.Teams;

namespace NutriApp;

public class App
{
    private readonly string userPath = $"{Persistence.UserDataPath}\\user.json";
    private readonly string datePath = $"{Persistence.DateDataPath}\\date.json";

    private ISaveSystem saveSystem;
    private HistoryController history;
    private GoalController goal;
    private WorkoutController workout;
    private FoodController food;
    private UserController userCtrl;
    private TeamController team;
    private DateTime date;
    private double dayLength;
    private Task<None> timerThread;

    public ISaveSystem SaveSyst => saveSystem;
    public HistoryController HistoryControl => history;
    public GoalController GoalControl => goal;
    public WorkoutController WorkoutControl => workout;
    public FoodController FoodControl => food;
    public UserController UserControl => userCtrl;
    public DateTime TimeStamp => date;

    public double DayLength
    {
        set => dayLength = value;
    }

    public App(double dayLength)
    {
        this.dayLength = dayLength;
        date = DateTime.Now;
        timerThread = new Task<None>(DayLoop);
        timerThread.Start();

        saveSystem = new SaveSystem();
        userCtrl = new UserController(saveSystem);
        workout = new WorkoutController();
        food = new FoodController(this);
        history = new HistoryController(this, saveSystem);
        goal = new GoalController(this, saveSystem);
        team = new TeamController(this, saveSystem);
        NotificationController.Instance.AppInstance = this;

        saveSystem.SubscribeSaveable(userCtrl);
        saveSystem.SubscribeSaveable(history);
        saveSystem.SubscribeSaveable(goal);
        saveSystem.SubscribeSaveable(team);
        
        
        saveSystem.LoadController();
        
        food.MealConsumeEvent += goal.ConsumeMealHandler;
        food.MealConsumeEvent += history.AddMeal;
    }
    

    public List<Workout.Workout> GetRecommendedWorkouts(string username)
        => workout.GenerateRecommendedWorkouts(history.GetWorkouts(username));

    public double GetTodaysCalories()
    {
        return -1d;
    }

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

        builder.Services.AddAuthentication(
                options => options.DefaultScheme = NutriAppAuthHandler.SCHEME_NAME)
            .AddScheme<NutriAppAuthSchemeOptions, NutriAppAuthHandler>(
                NutriAppAuthHandler.SCHEME_NAME, _ => { });
        builder.Services.AddAuthorization();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "NutriApp API", Version = "v1" });
            c.OperationFilter<AddHeaderOperationFilter>();
        });
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var webapp = builder.Build();

        if (webapp.Environment.IsDevelopment())
        {
            webapp.UseSwagger();
            webapp.UseSwaggerUI();
        }

        webapp.UseCors("AllowSpecificOrigin");
        webapp.MapControllers();
        
        webapp.Services
            .GetService<IHostApplicationLifetime>()?
            .ApplicationStopping
            .Register(() =>
            {
                var app = webapp.Services.GetService<App>();
                app.saveSystem.SaveController();
            });
            
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
        return new None();
    }
}