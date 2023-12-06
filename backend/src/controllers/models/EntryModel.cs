using System;

namespace NutriApp.Controllers.Models;

public struct EntryModel<T>
{
    public DateTime TimeStamp { get; set; }
    public T Value { get; set; }
}