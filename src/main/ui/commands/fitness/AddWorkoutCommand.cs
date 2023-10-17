using System;

namespace NutriApp;

class AddWorkoutCommand : Command<Workout.Workout>
{
    private App app;

    public AddWorkoutCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(Workout.Workout userinput)
    {
        app.HistoryControl.AddWorkout(userinput);
        onFinished?.Invoke();
    }
}