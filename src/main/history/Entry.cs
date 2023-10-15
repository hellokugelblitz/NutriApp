namespace NutriApp.History; 

public class Entry<T> {
    private DateTime timeStamp;
    private T value;
    public Entry(DateTime timeStamp, T value) {
        this.timeStamp = timeStamp;
        this.value = value;
    }

    public DateTime GetTimeStamp{get => timeStamp;}

    public T GetValue{get => value;}
}