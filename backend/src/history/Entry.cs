using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NutriApp.Save;

namespace NutriApp.History; 

public class Entry<T> : IHistorySaveable where T : IHistorySaveable, new()
{
    private DateTime timeStamp;
    private T value;
    public Entry(DateTime timeStamp, T value) {
        this.timeStamp = timeStamp;
        this.value = value;
    }

    public Entry()
    {
        value = new T();
    }

    [JsonProperty] public DateTime TimeStamp{get => timeStamp;}

    [JsonProperty] public T Value{get => value;}

    public string ToSaveString()
    {
        return timeStamp.ToString() + IHistorySaveable.HISTORY_SAVEABLE_SEPERATOR + value.ToSaveString();
    }

    public void FromSaveString(string str)
    {
        var time = str.Substring(0, str.IndexOf(IHistorySaveable.HISTORY_SAVEABLE_SEPERATOR));
        timeStamp = DateTime.Parse(time);
        value.FromSaveString(str.Substring(str.IndexOf(IHistorySaveable.HISTORY_SAVEABLE_SEPERATOR) + 1));
    }
}