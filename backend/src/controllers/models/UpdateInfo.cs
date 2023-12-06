using System;

namespace NutriApp.Controllers.Models;

public struct UpdateInfo
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
}