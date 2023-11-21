namespace NutriApp.Save;

public interface ISaveableController
{
    void SaveUser(string folderName);
    void LoadUser(string folderName);
    void SaveController();
    void LoadController();
}