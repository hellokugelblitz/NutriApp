using System;

namespace NutriApp;

public class User {
    private string name;
    private int height; //in inches
    private double weight;
    private DateTime birthday;

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
}