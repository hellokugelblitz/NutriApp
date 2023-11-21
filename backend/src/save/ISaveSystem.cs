namespace NutriApp.Save;

public interface ISaveSystem
{
    void SetFileType(IFileFormatSaver type);

    void Save(string folderName);

    void Load(string folderName);

    void SubscribeSaveable(ISaveable saveable);

    IFileFormatSaver GetFileSaver();

    string GetNewestFolder(string username);
}