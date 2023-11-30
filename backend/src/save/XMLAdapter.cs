using System.Collections.Generic;
using System.Xml;

namespace NutriApp.Save;

// https://www.c-sharpcorner.com/article/reading-and-writing-xml-in-C-Sharp/
public class XMLAdapter : IFileFormatSaver
{
    public void Save(string fileName, Dictionary<string, string> data)
    {
        XmlTextWriter textWriter = new XmlTextWriter(fileName, null) ;
        
        textWriter.WriteStartDocument();
        textWriter.WriteStartElement("Data");
        foreach (var pair in data)
        {
            textWriter.WriteStartElement(pair.Key);
            textWriter.WriteString(pair.Value);
            textWriter.WriteEndElement();
        }
        textWriter.WriteEndElement();
        textWriter.WriteEndDocument();
        textWriter.Close();
        
    }

    public Dictionary<string, string> Load(string fileName)
    {
        Dictionary<string, string> data = new();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(fileName);
       
        foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
        {
            if (node.NodeType == XmlNodeType.Element)
            {
                string nodeName = node.Name;
                string nodeValue = node.InnerText;

                data.Add(nodeName, nodeValue);
            }
        }
        
        return data;
    }

    public string GetFileType() => "xml";
}