using System;

namespace NutriApp.History; 

public class Entry<T> {
    private DateTime timeStamp;
    private T value;
    public Entry(DateTime timeStamp, T value) {
        this.timeStamp = timeStamp;
        this.value = value;
    }

    public DateTime TimeStamp{get => timeStamp;}

    public T Value{get => value;}
}