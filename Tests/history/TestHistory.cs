using NUnit.Framework;
using NutriApp;
using NutriApp.Food;
using NutriApp.History;

namespace Tests;

public class Tests {
    private App app;
    private HistoryController history;
    [SetUp]
    public void Setup() {
        app = new App();
        history = app.HistoryControl;
        history.AddMeal(new Meal());
        history.AddMeal(new Meal());
        
        history.SetWeight(100);
        history.SetWeight(150);

        history.AddCalories(app.TimeStamp);
    }

    [Test]
    public void Test1() {
        Assert.Pass();
    }
}