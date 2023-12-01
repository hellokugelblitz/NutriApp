using System;
namespace NutriApp.Undo;

class UndoSetWeightGoal : UndoCommand
{
    private App _app;
    private User _user;

    public UndoSetWeightGoal(App app, User user)
    {
        _app = app;
        _user = user;
    }

    public override void Execute()
    {
        onFinished?.Invoke();
    }
}