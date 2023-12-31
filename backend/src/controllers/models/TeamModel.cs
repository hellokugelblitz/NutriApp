using System;

namespace NutriApp.Controllers.Models;

public struct TeamModel
{
    public string Name { get; set; }
    public string[] Members { get; set; }
    public DateTime ChallengeStartDate { get; set; }
    public DateTime ChallengeEndDate { get; set; }
}