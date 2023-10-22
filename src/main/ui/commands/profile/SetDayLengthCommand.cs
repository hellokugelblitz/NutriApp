using System;

namespace NutriApp.UI;

class SetDayLengthCommand : Command<double>
{
    private App app;

    public SetDayLengthCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(double userinput)
    {
        app.DayLength = userinput;
        onFinished?.Invoke();
    }
}