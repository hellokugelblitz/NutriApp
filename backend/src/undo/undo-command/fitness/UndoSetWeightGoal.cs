using System;
namespace NutriApp.Undo;

public class UndoSetWeightGoal : UndoCommand
{
    private App _app;
    private User _user;
    private Goal.Goal _prevGoal;

    public UndoSetWeightGoal(App app, User user, Goal.Goal prevGoal)
    {
        _app = app;
        _user = user;
        _prevGoal = prevGoal;
    }

    public override void Execute()
    {
        _app.GoalControl.SetGoal(_prevGoal, _user.Name);

        onFinished?.Invoke();
    }
}