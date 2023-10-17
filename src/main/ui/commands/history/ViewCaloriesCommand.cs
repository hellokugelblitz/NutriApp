using System;

namespace NutriApp;

class ViewCaloriesCommand : Command<string>
{
    private App app;

    public ViewCaloriesCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(string userinput)
    {

    }
}