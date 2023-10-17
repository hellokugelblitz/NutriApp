using System;

namespace NutriApp.UI;

class ViewWorkoutsCommand : Command<string>
{
    private App app;

    public ViewWorkoutsCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(string userinput)
    {
        
    }
}