using System;
using NutriApp.Food;

namespace NutriApp.UI;

class GetShoppingListCommand : Command<None>
{
    private App app;

    public GetShoppingListCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(None userinput)
    {
        onFinished?.Invoke();
    }
}