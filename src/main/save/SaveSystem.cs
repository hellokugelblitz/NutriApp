using System.Collections.Generic;
using System.IO;

namespace NutriApp.Save;

public class SaveSystem : ISaveSystem
{
    private IFileFormatSaver _saver = null;
    private List<ISaveable> _saveables = null;
    
    public void SetFileType(IFileFormatSaver type)
    {
        _saver = type;
    }

    public void Save(string folderName)
    {
        foreach (var saveable in _saveables)
        {
            saveable.Save(folderName);
        }
    }

    public void Load(string folderName)
    {
        foreach (var saveable in _saveables)
        {
            saveable.Load(folderName);
        }    }

    public void SubscribeSaveable(ISaveable saveable)
    {
        _saveables.Add(saveable);
    }

    public IFileFormatSaver GetFileSaver()
    {
        return _saver;
    }


    public string GetNewestFolder(string username)
    {
        throw new System.NotImplementedException();
    }

    public static string GetFullPath(string foldername, string filename)
    {
        string extension = foldername.Split(":")[1];
        return Path.Join(foldername, filename + "." + extension);
    }
}