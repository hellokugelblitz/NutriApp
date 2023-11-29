using System.Collections.Generic;
using System.Xml;

namespace NutriApp.Save;

public class XMLAdapter : IFileFormatSaver
{
    public void Save(string fileName, Dictionary<string, string> data)
    {
        XmlDocument xml = new XmlDocument();
        
    }

    public Dictionary<string, string> Load(string fileName)
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(fileName);
        return null;
    }

    public string GetFileType() => "xml";
}