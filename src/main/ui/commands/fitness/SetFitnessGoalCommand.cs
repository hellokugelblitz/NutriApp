using System;
using NutriApp.Goal;

namespace NutriApp.UI;

class SetFitnessGoalCommand : Command<string>
{
    private App app;

    public SetFitnessGoalCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(string userinput)
    {
        if (userinput.Equals("yes"))
        {
            app.GoalControl.IncorporateFitness();
        }
    }
}