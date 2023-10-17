using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTSetFitnessGoalInvoker : CommandInvoker<Workout.Goal>
{
    private App app;

    public PTSetFitnessGoalInvoker(Command<Workout.Goal> command, App app) : base(command) 
    {
        this.app = app;
    }

    public override void Invoke()
    {

    }
}