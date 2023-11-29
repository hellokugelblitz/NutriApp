using System;
using NutriApp.Undo;

namespace NutriApp.UI;

class SetWeightGoalCommand : Command<Goal.Goal>
{
    private App _app;
    private Guid _sessionKey;

    public SetWeightGoalCommand(App app, Guid sessionKey)
    {
        _app = app;
        _sessionKey = sessionKey;
    }

    public override void Execute(Goal.Goal userinput)
    {
        Goal.Goal prevGoal = _app.GoalControl.Goal;

        _app.GoalControl.Goal = userinput;

        UndoCommand undoCommand = new UndoSetWeightGoal(_app.GoalControl, prevGoal);      
        _app.UserControl.AddUndoCommand(_sessionKey, undoCommand);

        onFinished?.Invoke();  
    }
}