using System.Collections.Generic;
using System.IO.Enumeration;

namespace NutriApp.Save;

public interface IFileFormatSaver
{
    void Save(string fileName, Dictionary<string, string> data);

    Dictionary<string, string> Load(string fileName);
    string GetFileType();
}