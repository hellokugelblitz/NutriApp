using System;
using System.Linq;
using NutriApp.History;

namespace NutriApp.Undo;

class UndoSetWeight : UndoCommand
{
    private App _app;
    private User _user;
    private DateTime _timestamp;

    public UndoSetWeight(App app, User user)
    {
        _app = app;
        _user = user;
    }

    public override void Execute()
    {
        
        onFinished?.Invoke();
    }
}