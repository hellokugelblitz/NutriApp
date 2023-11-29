using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace NutriApp.Save;

public class JSONAdapter : IFileFormatSaver
{
    public void Save(string fileName, Dictionary<string, string> data)
    {
        var str = JsonConvert.SerializeObject(data);
        File.WriteAllText(fileName, str);

    }

    public Dictionary<string, string> Load(string fileName)
    {
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(fileName));
    }

    public string GetFileType()
    {
        return "json";
    }
}