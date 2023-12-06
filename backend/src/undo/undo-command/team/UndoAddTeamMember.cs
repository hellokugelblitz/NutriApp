using System;
using NutriApp.Teams;

namespace NutriApp.Undo;

public class UndoAddTeamMember : UndoCommand
{
    private Team _team;
    private App _app;
    private string _username;

    public UndoAddTeamMember(App app, Team team, string username)
    {
        _team = team;
        _app = app;
        _username = username;
    }

    public override void Execute()
    {
        _app.TeamControl.RemoveMember(_username, _team.Name);
        onFinished?.Invoke();
    }
}


