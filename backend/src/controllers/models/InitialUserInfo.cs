using System;

namespace NutriApp.Controllers.Models;

public struct InitialUserInfo
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public DateTime Dob { get; set; }
}