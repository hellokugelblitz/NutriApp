using NutriApp.Food;

namespace NutriApp.UI;

class CreateMealCommand : Command<Meal>
{
    private App _app;
    
    public CreateMealCommand(App app)
    {
        _app = app;
    }
    
    public override void Execute(Meal userinput)
    {
        _app.FoodControl.AddMeal(userinput); 
        onFinished?.Invoke();
    }
}