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
    
    // POST api/teams
    [HttpPost]
    public ActionResult<Team> CreateTeam()
    {
        Teams.Team team = _app.TeamControl.CreateTeam(GetRequestBody(Request.Body)["teamName"]);
        User user = HttpContext.GetUser();

        team.AddMember(user.UserName);
        user.TeamName = team.Name;

        if (team is null) return Conflict();

        return Ok(new Team()
        {
            Name = team.Name,
            Members = team.Members,
            ChallengeStartDate = team.ChallengeStartDate,
            ChallengeEndDate = team.ChallengeEndDate
        });
    }

    // GET api/teams
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
    
    // POST api/teams/invite
    [HttpPost("invite")]
    public ActionResult<TeamInvite> InviteUser()
    {
        Dictionary<string, string> body = GetRequestBody(Request.Body);

        User user = HttpContext.GetUser();
        Teams.Team team = _app.TeamControl.GetTeam(user.TeamName);
        
        string inviteCode = _app.TeamControl.CreateInvite(body["username"], team.Name);

        return Ok(new TeamInvite() { Code = inviteCode });
    }
    
    // PUT api/teams/accept
    [HttpPut("accept")]
    public IActionResult AcceptInvite()
    {
        Dictionary<string, string> body = GetRequestBody(Request.Body);

        if (!_app.TeamControl.ValidateInviteCode(body["inviteCode"]))
            return Unauthorized();
    
        _app.TeamControl.AddMember(HttpContext.GetUser().UserName, body["inviteCode"]);
        return Ok();
    }
    
    // PUT api/teams/leave
    [HttpPut("leave")]
    public IActionResult LeaveTeam()
    {
        User user = HttpContext.GetUser();
        _app.TeamControl.RemoveMember(user.UserName, user.TeamName);
        return Ok();
    }
    
    // TODO: this should presumably be in each user's profile page? different API endpoint
    // // GET api/teams/workouts/{username}
    // [HttpGet("workouts/{username}")]
    // public ActionResult<IEnumerable<Entry<Models.Workout>>> GetWorkouts(string username)
    // {
        
    // }
    
    // POST api/teams/challenge
    [HttpPost("challenge")]
    public IActionResult CreateChallenge()
    {
        Dictionary<string, string> body = GetRequestBody(Request.Body);

        System.DateTime startDate = System.DateTime.Parse(body["startDate"]);
        Teams.Team team = _app.TeamControl.GetTeam(HttpContext.GetUser().TeamName);

        team.StartNewChallenge(startDate);
        return Ok();
    }
    
    // GET api/teams/challenge
    [HttpGet("challenge")]
    public ActionResult<Dictionary<string, int>> GetChallengeRanking()
    {
        Dictionary<string, int> response = _app.TeamControl.GetChallengeParticipants(HttpContext.GetUser().TeamName);
        return Ok(response);
    }

    private Dictionary<string, string> GetRequestBody(Stream body)
    {
        string requestString;

        using (StreamReader reader = new StreamReader(Request.Body))
            requestString = reader.ReadToEndAsync().Result;

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(requestString);
    }
}