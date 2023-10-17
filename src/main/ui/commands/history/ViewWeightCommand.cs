using System;

namespace NutriApp.UI;

class ViewWeightCommand : Command<string>
{
    private App app;

    public ViewWeightCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(string userinput)
    {
        
    }
}