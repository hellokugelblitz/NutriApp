using Microsoft.AspNetCore.Mvc;
using NutriApp.Teams;
using System.Collections.Generic;

namespace NutriApp.Api;

[ApiController]
[Route("teams")]
public class TeamApiController : ControllerBase
{
    private TeamController teamController;

    public TeamApiController(TeamController teamController)
    {
        this.teamController = teamController;
    }
}