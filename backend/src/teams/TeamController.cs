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
        return code;
    }

    /// <summary>
    /// Adds the user with the given username to the team corresponding with the given invite code. Returns true
    /// if the user was successfully added, false otherwise (e.g. an invalid invite code).
    /// </summary>
    public bool AddMember(string username, string inviteCode)
    {
        return false;
    }

    /// <summary>
    /// Removes the user with the given username from the team with the given name, if they are a member.
    /// </summary>
    public void RemoveMember(string username, string teamName)
    {

    }
}