namespace NutriApp.UI;

class ViewMealsCommand : Command<None>
{
    public override void Execute(None userinput)
    {
        onFinished?.Invoke();
    }
}