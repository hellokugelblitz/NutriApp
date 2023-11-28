namespace NutriApp.History;

public interface IHistorySaveable
{
    public static string HISTORY_SAVEABLE_SEPERATOR = ",";
    
    public string ToSaveString();
    public void FromSaveString(string str);
}