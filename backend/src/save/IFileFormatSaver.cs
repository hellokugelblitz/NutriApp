using System.Collections.Generic;
using System.IO.Enumeration;

namespace NutriApp.Save;

public interface IFileFormatSaver
{
    /// <summary>
    /// Saves the data given to the type of format the saver is for
    /// </summary>
    /// <param name="fileName">full filepath that you are saving to</param>
    /// <param name="data">dictionary of {name, value}. It will be used in tandem with ISaveObject</param>
    void Save(string fileName, Dictionary<string, string> data);

    /// <summary>
    /// Loads the data from a file to the dictionary
    /// </summary>
    /// <param name="fileName">full filepath that you are loading from</param>
    /// <returns>dictionary of {name, value}. It will be used in tandem with ISaveObject</returns>
    Dictionary<string, string> Load(string fileName);
    
    /// <summary>
    /// typecode that each IFileFormatSaver will return(ie: json)
    /// </summary>
    /// <returns>file type</returns>
    string GetFileType();
}