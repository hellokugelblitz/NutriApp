using System;
using System.Collections.Generic;
using NutriApp.Save;

namespace NutriApp.Teams;

public class Team : ISaveObject
{
    private string name;
    private static readonly string memberSeperator = ",";
    private List<string> members;  // stored as usernames
    private DateTime challengeStartDate;

    /// <summary>
    /// The display name of the team.
    /// </summary>
    public string Name => name;

    /// <summary>
    /// The usernames of users that are currently part of the team.
    /// </summary>
    public string[] Members => members.ToArray();

    /// <summary>
    /// The start date of the most recent weeklong challenge. Returns 1/1/1970 if no weeklong
    /// challenge was ever started.
    /// </summary>
    public DateTime ChallengeStartDate => challengeStartDate;

    /// <summary>
    /// The end date of the most recent weeklong challenge. Returns 1/1/1970 if no weeklong
    /// challenge was ever started.
    /// </summary>
    public DateTime ChallengeEndDate => challengeStartDate == DateTime.UnixEpoch ? DateTime.UnixEpoch : challengeStartDate.AddDays(7);

    public Team(string name)
    {
        this.name = name;
        this.members = new List<string>();
    }

    /// <summary>
    /// Adds a new member to the team.
    /// </summary>
    public void AddMember(string username)
    {
        members.Add(username);
    }

    /// <summary>
    /// Removes a user from the team if they are a member of the team.
    /// </summary>
    public void RemoveMember(string username)
    {
        members.Remove(username);
    }
    
    /// <summary>
    /// Starts a weeklong challenge for the team on the given date.
    /// </summary>
    public void StartNewChallenge(DateTime startDate) => challengeStartDate = startDate;

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> data = new();
        data["Name"] = name;
        string str = "";
        members.ForEach((ele) => str += ele + memberSeperator);
        str = str.Substring(0, str.Length - memberSeperator.Length);
        data["Members"] = str;
        return data;
    }

    public void FromDictionary(Dictionary<string, string> data)
    {
        name = data["Name"];
        
        
    }
}