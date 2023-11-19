using System;
using NutriApp.Food;

namespace NutriApp.UI;

class SearchingIngredientsCommand : Command<string>
{
    private App app;
    private SearchUpdater _updater;

    public SearchingIngredientsCommand(App app, SearchUpdater updater)
    {
        this.app = app;
        _updater = updater;
    }

    public override void Execute(string userinput)
    {
        _updater.DisplaySearch(userinput);
        onFinished?.Invoke();
    }
}