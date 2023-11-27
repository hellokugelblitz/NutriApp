using System;

namespace NutriApp.Controllers.Models;

public struct Entry<T>
{
    public DateTime TimeStamp { get; set; }
    public T Value { get; set; }
}