using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NutriApp.Save;
using NutriApp.Notifications;

namespace NutriApp;

public class User : ISaveObject
{
    private List<Notification> notifications;

    public string UserName { get; private set; }
    public string Name { get; private set; }

    public int Height { get; private set; }
    public DateTime Birthday { get; private set; }
    public string Bio { get; private set; }
    public string TeamName { get; set; }
    
    public Notification[] Notifications => notifications?.ToArray() ?? Array.Empty<Notification>();

    public User()
    {
        notifications = new List<Notification>();
    }
    
    public User(string username, string name, int height, DateTime birthday, string bio, string teamName="")
    {
        notifications = new List<Notification>();

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

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> data = new();
        data["UserName"] = UserName;
        data["Name"] = Name;
        data["Height"] = Height.ToString();
        data["Birthday"] = Birthday.ToString();
        data["Bio"] = Bio;
        data["TeamName"] = TeamName;
        
        return data;
    }

    public void FromDictionary(Dictionary<string, string> data)
    {
        UserName = data["UserName"];
        Name = data["Name"];
        Height = Int32.Parse(data["Height"]);
        Birthday = DateTime.Parse(data["Birthday"]);
        Bio = data["Bio"];
        TeamName = data["TeamName"];    
    }

    public void ReceiveNotification(Notification notification) => notifications.Add(notification);

    // public T FromDictionary<T>(Dictionary<string, string> data) where T: ISaveObject
    // {
    //     return new User(
    //         data["username"],
    //         data["name"],
    //         Int32.Parse(data["height"]),
    //         DateTime.Parse(data["birthday"]),
    //         data["bio"],
    //         data["teamName"]);
    // }
}