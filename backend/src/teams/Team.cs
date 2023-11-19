using System;
using System.Collections.Generic;

namespace NutriApp.Teams;

public class Team
{
    private string name;
    private List<User> members;
    private DateTime challengeStartDate;

    /// <summary>
    /// The display name of the team.
    /// </summary>
    public string Name => name;

    /// <summary>
    /// The users that are currently part of the team.
    /// </summary>
    public User[] Members => members.ToArray();

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

    /// <summary>
    /// All members that have participated in the most recent weeklong challenge, ranked from most minutes to least.
    /// Dictionary key is username, value is number of workout minutes logged.
    /// </summary>
    public Dictionary<string, int> ChallengeParticipants
    {
        get
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            // TODO: get user workout histories and aggregate them here
            return result;
        }
    }


    public Team(string name)
    {
        this.name = name;
    }

    /// <summary>
    /// Adds a new member to the team.
    /// </summary>
    public void AddMember(User user) => members.Add(user);

    /// <summary>
    /// Removes a user from the team if they are a member of the team.
    /// </summary>
    public void RemoveMember(User user) => members.Remove(user);

    /// <summary>
    /// Starts a weeklong challenge for the team on the given date.
    /// </summary>
    public void StartNewChallenge(DateTime startDate) => challengeStartDate = startDate;
}