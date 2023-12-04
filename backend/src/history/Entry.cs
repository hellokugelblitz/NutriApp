using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NutriApp.Save;

namespace NutriApp.History; 

public class Entry<T>
{
    private DateTime timeStamp;
    private T value;
    
    public Entry(DateTime timeStamp, T value) {
        this.timeStamp = timeStamp;
        this.value = value;
    }

    public DateTime TimeStamp => timeStamp;

    public T Value => value;

    public override bool Equals(object obj)
    {
        Entry<T> other = obj as Entry<T>;
        return other is not null && value.Equals(other.value) && timeStamp.ToString() ==  other.timeStamp.ToString();
    }
    
    
}