using System;
using NutriApp.Goal;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTSetWeightGoalInvoker : CommandInvoker<Goal.Goal>
{
    private App app;

    public PTSetWeightGoalInvoker(Command<Goal.Goal> command, App app) : base(command) 
    { 
        this.app = app;
    }

    public override void Invoke()
    {
        Console.WriteLine("please enter a target weight");
        double targetWeight = Double.Parse(Console.ReadLine());
        double weightChange = targetWeight - app.HistoryControl.CurrentWeight;
        Goal.Goal goal;
        if (weightChange < -5)
        {
            goal = new LoseWeightGoal(app.GoalControl, targetWeight);
        }
        else if(weightChange <= 5)
        {
            goal = new MaintainWeightGoal(app.GoalControl, targetWeight);
        }
        else
        {
            goal = new GainWeightGoal(app.GoalControl, targetWeight);
        }

        command.Execute(goal);
    }
}