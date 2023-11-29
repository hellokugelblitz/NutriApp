using System;
using System.Collections.Generic;
using System.IO;

namespace NutriApp.Save;

public class SaveSystem : ISaveSystem
{
    public static string SavePath => "data\\saves";
    public static string Seperator => "-";

    private IFileFormatSaver _saver = new JSONAdapter();
    private List<ISaveableController> _saveables = new ();
    
    public void SetFileType(IFileFormatSaver type)
    {
        _saver = type;
    }

    public void LoadController()
    {
        foreach (var saveable in _saveables)
        {
            saveable.LoadController();
        }
    }

    public void SaveController()
    {
        foreach (var saveable in _saveables)
        {
            saveable.SaveController();
        }    
    }

    public void SaveUser(string folderName)
    {
        folderName = SavePath + "\\" + folderName;//need to add the folder path to the foldername
        Directory.CreateDirectory(folderName);
        foreach (var saveable in _saveables)
        {
            saveable.SaveUser(folderName);
        }
    }

    public void LoadUser(string folderName)
    {
        foreach (var saveable in _saveables)
        {
            saveable.LoadUser(folderName);
        }    
    }

    public void AddNewUser(User user)
    {
        foreach (var saveable in _saveables)
        {
            saveable.AddNewUser(user);
        }  
    }
    
    public void SubscribeSaveable(ISaveableController saveableController)
    {
        _saveables.Add(saveableController);
    }

    public IFileFormatSaver GetFileSaver()
    {
        return _saver;
    }

    /// <summary>
    /// Creates a folder name with the format {username}:{filetype}:{file number}
    /// </summary>
    /// <param name="username">username you want to create save for</param>
    /// <param name="fileType">file type(json, xml, csv) if empty it defaults to the current IFileFormatSaver's type</param>
    /// <returns></returns>
    public string CreateNewestFolderName(string username, string fileType = "")
    {
        if (fileType == "") fileType = _saver.GetFileType();
        var strs = Directory.GetDirectories(SavePath);
        int largestNum = -1;
        foreach (var file in Directory.GetDirectories(SavePath))
        {
            var split = SplitFileName(file);
            
            if (Int32.Parse(split[2]) > largestNum && split[0] == username)
            {
                largestNum = Int32.Parse(split[2]);
            }
        }

        return $"{username}{Seperator}{fileType}{Seperator}{largestNum + 1}";
    } 
    
    /// <summary>
    /// gets the folder with the largest number in the identifier portion
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
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
                largestNum = Int32.Parse(split[2]);
                newest = file;
            }
        }
        
        return  newest;
    }

    public static string GetUsernameFromFile(string path) => SplitFileName(path)[0];

    public static string[] SplitFileName(string path)
    {
        string fileName = Path.GetFileName(path);
        return fileName.Split(Seperator);
    }

    //gets the 
    public static string GetFullPath(string foldername, string filename)
    {
        string extension = foldername.Split("-")[1];
        return Path.Join(foldername, filename + "." + extension);
    }
}