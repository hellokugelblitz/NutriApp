namespace NutriApp.UI;

class ViewRecipesCommand : Command<None>
{
    public override void Execute(None userinput)
    {
        onFinished?.Invoke(); // Front end will view the target calories
    }
}