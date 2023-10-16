using System;

namespace NutriApp.History; 

//Stores a value and its time stamps. The value is the type you pass into the generic.
public class Entry<T> 
{
    private DateTime timeStamp;
    private T value;
    
    public Entry(DateTime timeStamp, T value) 
    {
        this.timeStamp = timeStamp;
        this.value = value;
    }

    public DateTime TimeStamp => timeStamp;

    public T Value => value;
}