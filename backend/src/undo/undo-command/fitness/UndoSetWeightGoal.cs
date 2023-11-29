using System;
using NutriApp.Goal;

namespace NutriApp.Undo;

class UndoSetWeightGoal : UndoCommand
{
    private GoalController _goalController;
    private Goal.Goal _goal;

    public UndoSetWeightGoal(GoalController goalController, Goal.Goal goal)
    {
        _goalController = goalController;
        _goal = goal;
    }

    public override void Execute()
    {
        _goalController.Goal = _goal;
        
        onFinished?.Invoke();
    }
}