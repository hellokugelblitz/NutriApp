using System;
using System.IO;
using Newtonsoft.Json;

namespace NutriApp;

public class User {
    public string UserName { get; }
    public string Name { get; }

    public int Height { get; }
    public DateTime Birthday { get; }
    public string Bio { get; }
    public string TeamName { get; }

    public User(string username, string name, int height, DateTime birthday, string bio, string teamName="")
    {
        UserName = username;
        Name = name;
        Height = height;
        Birthday = birthday;
        Bio = bio;
        TeamName = teamName;
    }

    //doing everything by username since we are guaranteeing that is unique
    public override bool Equals(object obj)
    {
        if (obj is null || obj.GetType() != typeof(User)) return false;
        return ((User)obj).UserName == UserName;
    }

    public override int GetHashCode()
    {
        return UserName.GetHashCode();
    }
}