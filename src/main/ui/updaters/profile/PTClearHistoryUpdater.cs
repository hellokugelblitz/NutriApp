using System;

namespace NutriApp.UI;

class PTClearHistoryUpdater : Updater 
{
    public PTClearHistoryUpdater(ClearHistoryCommand clearHistoryCommand, App app) : base(app) {
        clearHistoryCommand.Subscribe(Update);
    }

    public override void Update()
    {
        Console.WriteLine("History cleared.");
    }
}