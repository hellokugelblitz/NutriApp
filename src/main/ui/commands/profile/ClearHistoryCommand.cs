using System;

namespace NutriApp.UI;

class ClearHistoryCommand : Command<string>
{
    private App app;

    public ClearHistoryCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(string userinput)
    {

    }
}