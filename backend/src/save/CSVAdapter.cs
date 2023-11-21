using System;
using System.Collections.Generic;
using System.IO;

namespace NutriApp.Save;

public class CSVAdapter : IFileFormatSaver
{
    private static readonly string COMMA = ",";
    public void Save(string fileName, Dictionary<string, string> data)
    {
        string headers = "";
        string values = "";

        foreach (var pair in data)
        {
            headers += pair.Key + ",";
            values += pair.Value + ",";
        }

        headers = headers.Substring(0, headers.Length - 1);
        values = values.Substring(0, values.Length - 1);
        
        File.WriteAllLines(fileName, new[] {headers, values});

    }

    public Dictionary<string, string> Load(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        if (lines.Length > 2)
        {
            throw new Exception("csv file has too many lines");
        }
        string[] headers = lines[0].Split(COMMA);
        string[] values = lines[1].Split(COMMA);
        Dictionary<string, string> data = new();
        for (int i = 0; i < headers.Length; i++)
        {
            data[headers[i]] = values[i];
        }

        return data;
    }

    public string GetFileType() => "csv";
}