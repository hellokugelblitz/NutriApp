using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NutriApp.Controllers.Models;
using NutriApp;
using System.IO;
using NutriApp.Controllers.Middleware;

namespace NutriApp.Controllers;

[Route("api/Teams")]
[ApiController]
[Authorize]
public class TeamsApiController : ControllerBase
{
    private readonly App _app;
    
    public TeamsApiController(App app)
    {
        _app = app;
    }
    
    // POST api/Teams
    [HttpPost]
    public ActionResult<Team> CreateTeam()
    {
        string requestString;

        using (StreamReader reader = new StreamReader(Request.Body))
            requestString = reader.ReadToEndAsync().Result;

        Dictionary<string, string> request = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestString);
        Teams.Team team = _app.TeamControl.CreateTeam(request["teamName"]);

        if (team is null) return Conflict();

        return Ok(new Team()
        {
            Name = team.Name,
            Members = team.Members,
            ChallengeStartDate = team.ChallengeStartDate,
            ChallengeEndDate = team.ChallengeEndDate
        });
    }

    // GET api/Teams
    [HttpGet]
    public ActionResult<Team> GetTeam()
    {
        User user = HttpContext.GetUser();
        Teams.Team team = _app.TeamControl.GetTeam(user.TeamName);

        return team is null ? NoContent() : Ok(new Team()
        {
            Name = team.Name,
            Members = team.Members,
            ChallengeStartDate = team.ChallengeStartDate,
            ChallengeEndDate = team.ChallengeEndDate
        });
    }
    
    // POST api/Teams/invite/{username}
    [HttpPost("invite/{username}")]
    public ActionResult<TeamInvite> InviteUser(string username)
    {
        return new TeamInvite { Code = "nutricode" };
    }
    
    // PUT api/Teams/invite/accept/{inviteCode}
    [HttpPut("invite/accept/{inviteCode}")]
    public ActionResult<Team> AcceptInvite(string inviteCode)
    {
        return new Team { Name = "NutriTeam" };
    }
    
    // PUT api/Teams/leave
    [HttpPut("leave")]
    public IActionResult LeaveTeam()
    {
        return Ok();
    }
    
    // GET api/Teams/workouts/{username}
    [HttpGet("workouts/{username}")]
    public ActionResult<IEnumerable<Entry<Models.Workout>>> GetWorkouts(string username)
    {
        // Some dummy workout structs
        Entry<Models.Workout>[] workouts =
        {
            new()
                { Value = new() { Name = "Morning Jog", Minutes = 60, Intensity = 10 } },
            new()
                { Value = new() { Name = "Afternoon Jog", Minutes = 60, Intensity = 10 } },
            new()
                { Value = new() { Name = "Evening Jog", Minutes = 60, Intensity = 10 } }
        };
        
        return workouts;
    }
    
    // POST api/Teams/challenge
    [HttpPost("challenge")]
    public IActionResult CreateChallenge()
    {
        return Ok();
    }
    
    // GET api/Teams/challenge
    [HttpGet("challenge")]
    public ActionResult<List<UserProfile>> GetChallengeRanking()
    {
        // Some dummy user profile structs
        UserProfile[] users =
        {
            new() { Name = "Dan Donchuk" },
            new() { Name = "Raynard Miot" },
            new() { Name = "Danny Gramowski" },
            new() { Name = "Austyn Wright" },
            new() { Name = "Jack Lindsey-Noble" }
        };
        
        return users.ToList();
    }
}