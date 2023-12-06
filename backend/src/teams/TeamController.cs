using System;
using System.Collections.Generic;
using System.IO;
using NutriApp.History;
using NutriApp.Notifications;
using System.Linq;
using Newtonsoft.Json;
using NutriApp.Save;

namespace NutriApp.Teams;

public class TeamController : ISaveableController
{
    private const int INVITE_CODE_LENGTH = 8;

    private App app;
    private List<Team> teams;
    private Dictionary<string, Team> inviteCodes;
    private ISaveSystem saveSystem;
    
    public TeamController(App app, ISaveSystem saveSystem)
    {
        this.app = app;
        teams = new List<Team>();
        inviteCodes = new Dictionary<string, Team>();
        this.saveSystem = saveSystem;
    }

    /// <summary>
    /// Returns the team with the given unique name.
    /// </summary>
    public Team GetTeam(string name)
    {
        foreach (Team team in teams)
            if (team.Name == name)
                return team;

        return null;
    }

    /// <summary>
    /// Creates a new team with the given name. Returns true if successfully created, false otherwise
    /// (typically if name is already taken).
    /// </summary>
    public Team CreateTeam(string name)
    {
        if (GetTeam(name) != null) return null;

        Team team = new Team(name);

        teams.Add(team);
        return team;
    }

    /// <summary>
    /// Generates an invite code and sends a notification to the given user, inviting them to join
    /// the given team. Returns the new invite code.
    /// </summary>
    public string CreateInvite(string username, string teamName)
    {
        Team team = GetTeam(teamName);
        string code = "";

        for (int i = 0; i < INVITE_CODE_LENGTH; i++)
            code += (char)(new Random().NextInt64('a', 'z'));  // Generate the next random character (a-z)

        inviteCodes.Add(code, team);

        User recipient = app.UserControl.GetUser(username);

        // TODO: actually incorporate invite code into notification
        NotificationController.Instance.CreateNotification(
            $"You have been invited to join team {teamName}",
            $"/protected/teams/join/{code}",
            "Accept",
            new User[] { recipient });
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
        if (!inviteCodes.ContainsKey(inviteCode)) return;

        User user = app.UserControl.GetUser(username);
        Team team = inviteCodes[inviteCode];

        user.TeamName = team.Name;
        team.AddMember(username);
        inviteCodes.Remove(inviteCode);
    }

    /// <summary>
    /// Removes the user with the given username from the team with the given name, if they are a member.
    /// </summary>
    public void RemoveMember(string username, string teamName)
    {
        User user = app.UserControl.GetUser(username);
        Team team = GetTeam(teamName);

        user.TeamName = "";
        team.RemoveMember(username);
    }

    /// <summary>
    /// Retrieves a dictionary of usernames to the number of workout minutes logged during the current
    /// weeklong challenge period; sorted by minutes logged, descending.
    /// </summary>
    public Dictionary<string, int> GetChallengeParticipants(string teamName)
    {
        Dictionary<string, int> result = new Dictionary<string, int>();
        Team team = GetTeam(teamName);

        if (team is null) return null;

        foreach (string username in team.Members)
        {
            if (!result.ContainsKey(username))
                    result.Add(username, 0);

            Entry<Workout.Workout>[] workoutEntries = app.HistoryControl.GetWorkouts(username).ToArray();
            
            foreach (Entry<Workout.Workout> entry in workoutEntries)
            {
                if (entry.TimeStamp < team.ChallengeStartDate || entry.TimeStamp > team.ChallengeEndDate)
                    continue;
                
                result[username] += entry.Value.Minutes;
            }
        }

        result = result.OrderByDescending(d => d.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        return result;
    }

    public void SaveUser(string folderName) { }

    public void LoadUser(string folderName) { }

    public void SaveController()
    {
        string filePath = SaveSystem.SavePath + "\\teams.json";
        using (var file = File.CreateText(filePath))
        {
            file.WriteLine(JsonConvert.SerializeObject(teams.Select((team) => team.ToDictionary()).ToArray()));
        }
    }

    public void LoadController()
    {
        string filePath = SaveSystem.SavePath + "\\teams.json";
        if(!File.Exists(filePath))return;
        
        using (var file = File.OpenText(filePath))
        {
            teams = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(
                file.ReadLine()).Select(dict =>
                {
                    Team team = new Team("");
                    team.FromDictionary(dict);
                    return team;
                })
                .ToList();
            
        }
    }

    public void AddNewUser(User user) { }
}