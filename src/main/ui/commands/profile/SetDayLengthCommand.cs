using System;

namespace NutriApp.UI;

class SetDayLengthCommand : Command<string>
{
    private App app;

    public SetDayLengthCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(string userinput)
    {

    }
}