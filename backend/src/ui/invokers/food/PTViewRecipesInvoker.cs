namespace NutriApp.UI;

class PTViewRecipesInvoker: CommandInvoker<None>
{
    public PTViewRecipesInvoker(Command<None> command) : base(command) { }
    public override void Invoke()
    {
        command.Execute(null);
    }
}