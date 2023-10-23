using System;
using System.IO;
using Newtonsoft.Json;

namespace NutriApp;

public class User {
    private readonly string userPath = $"{Persistence.UserDataPath}\\user.json";

    [JsonProperty] private string name;
    [JsonProperty] private int height; //in inches
    [JsonProperty] private double weight;
    [JsonProperty] private DateTime birthday;

    public User(string name, int height, double weight, DateTime birthday) {
        this.name = name;
        this.height = height;
        this.weight = weight;
        this.birthday = birthday;
    }

    public string GetName{get => name;}
    public int GetHeight{get => height;}
    public double GetWeight{get => weight;}
    public DateTime GetBirthday{get => birthday;}

    public void Save()
    {
        // Write the goal to a JSON file for persistence
        File.WriteAllText(userPath, JsonConvert.SerializeObject(this));
    }
    public void Load()
    {
        // Don't do anything if data files don't exist yet (e.g. first startup)
        if (!File.Exists(userPath))
            return;

        // Read the goal from a JSON file for persistence
        string json = File.ReadAllText(userPath);
        JsonConvert.PopulateObject(json, this);
    }
}