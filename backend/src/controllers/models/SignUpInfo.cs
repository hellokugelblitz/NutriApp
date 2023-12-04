using System;

namespace NutriApp.Controllers.Models;

public struct SignUpInfo
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public int Height { get; set; }
    public DateTime Birthday { get; set; }
    public double CurrentWeight { get; set; }
    public double WeightGoal { get; set; }
}