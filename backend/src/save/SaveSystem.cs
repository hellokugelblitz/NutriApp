using System;
using System.Collections.Generic;
using System.IO;

namespace NutriApp.Save;

public class SaveSystem : ISaveSystem
{
    public static string SavePath => "data\\saves";
    public static string Seperator => "-";

    private IFileFormatSaver _saver = null;
    private List<ISaveable> _saveables = new ();
    
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

    public String[] SplitFileName(string path)
    {
        string fileName = Path.GetFileName(path);
        fileName = fileName.Substring(0, fileName.IndexOf("."));
        return fileName.Split(Seperator);
    }

    public string GetNewestFolder(string username)
    {
        var strs = Directory.GetDirectories(SavePath);
        string newest = "";
        int largestNum = -1;
        foreach (var file in Directory.GetDirectories(SavePath))
        {
            var split = SplitFileName(file);
                        
            
            if (Int32.Parse(split[2]) > largestNum && split[0] == username)
            {
                newest = file;
            }
        }
        
        return  newest;
    }

    public static string GetFullPath(string foldername, string filename)
    {
        string extension = foldername.Split("-")[1];
        return Path.Join(foldername, filename + "." + extension);
    }
}