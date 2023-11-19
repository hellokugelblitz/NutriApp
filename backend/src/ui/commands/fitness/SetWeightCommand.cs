namespace NutriApp.UI;

class SetWeightCommand : Command<double>
{
    private App _app;
    
    public SetWeightCommand(App app)
    {
        _app = app;
    }
    
    public override void Execute(double userinput)
    {
        _app.HistoryControl.SetWeight(userinput);
        _app.GoalControl.CompareUserWeightToGoal();
    }
}