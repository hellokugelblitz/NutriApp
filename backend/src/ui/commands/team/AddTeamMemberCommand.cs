using System;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Serialization;
using NutriApp.Teams;
using NutriApp.Undo;

namespace NutriApp.UI;

class AddTeamMemberCommand : Command<string>
{
    private App _app;
    private Guid _sessionKey;
    private string _teamName;

    public AddTeamMemberCommand(App app, Guid sessionKey, string teamName)
    {
        _app = app;
        _sessionKey = sessionKey;
        _teamName = teamName;
    }

    public override void Execute(string username)
    {
        var team = _app.TeamControl.GetTeam(_teamName);
        var user = _app.UserControl.GetUser(username);
        if (team == null)
        {
            throw new Exception("The team name given does not exist.");
        }
        else if (user == null)
        {
            throw new Exception("The user with the specified username does not exist.");
        }
        else
        {
            _app.TeamControl.AddMember(user.UserName, _teamName);

            UndoCommand undoCommand = new UndoAddTeamMember(team, _app.TeamControl, user.Name);
            _app.UserControl.AddUndoCommand(_sessionKey, undoCommand);
            onFinished?.Invoke();
        }
    }
}