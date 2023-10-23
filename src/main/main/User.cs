using System;
using System.IO;
using Newtonsoft.Json;

namespace NutriApp;

public class User {
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

    public string Name => name;
    public int Height => height;
    public double Weight => weight;
    public DateTime Birthday => birthday;
}