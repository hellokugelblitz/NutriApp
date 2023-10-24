using System;
using System.IO;
using Newtonsoft.Json;

namespace NutriApp;

public class User {
    public string Name { get; }
    public int Height { get; }
    public DateTime Birthday { get; }

    public User(string name, int height, DateTime birthday) {
        Name = name;
        Height = height;
        Birthday = birthday;
    }
}