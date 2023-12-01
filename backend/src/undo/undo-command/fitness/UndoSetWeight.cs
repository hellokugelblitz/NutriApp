using System;
using System.Linq;
namespace NutriApp.Undo;

class UndoSetWeight : UndoCommand
{
    private App _app;
    private User _user;

    public UndoSetWeight(App app, User user)
    {
        _app = app;
        _user = user;
    }

    public override void Execute()
    {
        _app.HistoryControl.RemoveLastestWeight(_user.Name);
        
        onFinished?.Invoke();
    }
}