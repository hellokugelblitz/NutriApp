using System;
using NutriApp.History;

namespace NutriApp.UI;

class PTViewWeightUpdater : Updater
{
    public PTViewWeightUpdater(ViewWeightCommand viewWeightCommand, App app): base(app)
    {
        viewWeightCommand.Subscribe(Update);
    }
 
    public override void Update()
    {
        Console.WriteLine("View Weight");

        foreach (var weight in _app.HistoryControl.Weights)
        {
            PrintEntry(weight);
        }
    }

    private void PrintEntry(Entry<double> weight)
    {
        Console.WriteLine($"{weight.TimeStamp}| {weight.Value}");
    }
}