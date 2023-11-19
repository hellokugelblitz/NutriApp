using System;
using Newtonsoft.Json;

namespace NutriApp.History; 

public class Entry<T> {
    private DateTime timeStamp;
    private T value;
    public Entry(DateTime timeStamp, T value) {
        this.timeStamp = timeStamp;
        this.value = value;
    }

    [JsonProperty] public DateTime TimeStamp{get => timeStamp;}

    [JsonProperty] public T Value{get => value;}
}