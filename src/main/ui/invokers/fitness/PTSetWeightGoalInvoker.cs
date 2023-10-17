using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTSetWeightGoalInvoker : CommandInvoker<Workout.Goal>
{
    private App app;

    public PTSetWeightGoalInvoker(Command<Workout.Goal> command, App app) : base(command) 
    { 
        this.app = app;
    }

    public override void Invoke()
    {

    }
}