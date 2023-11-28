namespace NutriApp.UI;



class EnterUserInfoCommand : Command<(User, double, double)>
{
    private App app;

    public EnterUserInfoCommand(App app)
    {
        this.app = app;
    }

    public override void Execute((User, double, double) userinput)
    {
        // app.User = userinput.Item1;

        var userWeight = userinput.Item2;
        var targetWeight = userinput.Item3;

        app.HistoryControl.SetWeight(userWeight);
        app.GoalControl.SetGoalBasedOnWeightDifference(targetWeight);
    }
}