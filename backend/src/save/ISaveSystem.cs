namespace NutriApp.Save;

public interface ISaveSystem
{
    void SetFileType(IFileFormatSaver type);

    void LoadController();
    void SaveController();

    void SaveUser(string folderName);

    void LoadUser(string folderName);
    public void AddNewUser(User user);


    void SubscribeSaveable(ISaveableController saveableController);

    IFileFormatSaver GetFileSaver();

    string GetNewestFolder(string username);
    public string CreateNewestFolderName(string username, string fileType = "");

}