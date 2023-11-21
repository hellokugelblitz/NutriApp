namespace NutriApp.Save;

public interface ISaveable
{
    void Save(string folderName);
    void Load(string folderName);
}