using System;
using System.IO;

public static class Persistence
{
    private static string FOOD_PATH = "data\\food\\";
    private static string HISTORY_PATH = "data\\history\\";
    private static string GOALS_PATH = "data\\goals\\";
    private static string USER_PATH = "data\\user\\";
    private static string DATE_PATH = "data\\date\\";
    
    public static string FoodDataPath
    {
        get
        {
            Directory.CreateDirectory(FOOD_PATH);
            return FOOD_PATH;
        }
    }

    public static string HistoryDataPath
    {
        get
        {
            Directory.CreateDirectory(HISTORY_PATH);
            return HISTORY_PATH;
        }
    }

    public static string GoalsDataPath
    {
        get
        {
            Directory.CreateDirectory(GOALS_PATH);
            return GOALS_PATH;
        }
    }

    public static string UserDataPath
    {
        get
        {
            Directory.CreateDirectory(USER_PATH);
            return USER_PATH;
        }
    }
    
    public static string DateDataPath
    {
        get
        {
            Directory.CreateDirectory(DATE_PATH);
            return DATE_PATH;
        }
    }
}