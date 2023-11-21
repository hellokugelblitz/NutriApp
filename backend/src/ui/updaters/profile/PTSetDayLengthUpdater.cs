using System;

namespace NutriApp.UI;

class PTSetDayLengthUpdater : Updater
{
    public PTSetDayLengthUpdater(SetDayLengthCommand setDayLengthCommand, App app) : base(app)
    {
        setDayLengthCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine("Day length set.");
    }
}