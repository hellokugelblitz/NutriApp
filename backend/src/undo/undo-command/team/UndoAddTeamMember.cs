using System;
using NutriApp.Teams;

namespace NutriApp.Undo;

public class UndoAddTeamMember : UndoCommand
{
    private Team _team;
    private TeamController _controller;
    private string _username;

    public UndoAddTeamMember(Team team, TeamController controller, string username)
    {
        _team = team;
        _controller = controller;
        _username = username;
    }

    public override void Execute()
    {
        _controller.RemoveMember(_username, _team.Name);
        onFinished?.Invoke();
    }
}


