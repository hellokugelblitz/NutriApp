namespace NutriApp.UI;

class PTQuitInvoker : CommandInvoker<None>
{

    public PTQuitInvoker(Command<None> command) : base(command) { }
    
    public override void Invoke()
    {
        command.Execute(null);
    }
}