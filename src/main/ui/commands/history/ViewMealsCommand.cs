using System;

namespace NutriApp;

class ViewMealsCommand : Command<string>
{
    private App app;

    public ViewMealsCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(string userinput)
    {

    }
}