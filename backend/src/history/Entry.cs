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
}