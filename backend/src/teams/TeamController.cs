using System;
using System.Collections.Generic;
using System.Linq;

namespace NutriApp.Teams;

public class TeamController
{
    private const int INVITE_CODE_LENGTH = 8;

    private App app;
    private List<Team> teams;
    private Dictionary<string, Team> inviteCodes;

    public TeamController(App app)
    {
        this.app = app;
        teams = new List<Team>();
        inviteCodes = new Dictionary<string, Team>();
    }

    /// <summary>
    /// Returns the team with the given unique name.
    /// </summary>
    public Team GetTeam(string name) => teams.First(team => team.Name == name);

    /// <summary>
    /// Creates a new team with the given name.
    /// </summary>
    public void CreateTeam(string name) => teams.Add(new Team(name));

    /// <summary>
    /// Generates an invite code and sends a notification to the given user, inviting them to join
    /// the given team. Returns the new invite code.
    /// </summary>
    public string CreateInvite(string username, string teamName)
    {
        Team team = GetTeam(teamName);
        string code = string.Empty;

        for (int i = 0; i < INVITE_CODE_LENGTH; i++)
            code += (char)(new Random().NextInt64('a', 'z'));  // Generate the next random character (a-z)

        inviteCodes.Add(code, team);

        // TODO: send notification to user
        return code;
    }

    /// <summary>
    /// Returns whether an invite code is valid or not.
    /// </summary>
    public bool ValidateInviteCode(string inviteCode) => inviteCodes.ContainsKey(inviteCode);

    /// <summary>
    /// Adds the user with the given username to the team corresponding with the given invite code.
    /// Note that this will remove that code from the pool of valid invite codes.
    /// </summary>
    public void AddMember(string username, string inviteCode)
    {
        User user = app.User.GetUserByUsername(username);
        Team team = inviteCodes[inviteCode];

        team.AddMember(user);
        inviteCodes.Remove(inviteCode);
    }

    /// <summary>
    /// Removes the user with the given username from the team with the given name, if they are a member.
    /// </summary>
    public void RemoveMember(string username, string teamName)
    {
        User user = app.User.GetUserByUsername(username);
        Team team = GetTeam(teamName);
        team.RemoveMember(user);
    }
}