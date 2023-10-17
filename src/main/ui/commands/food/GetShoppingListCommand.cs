using System;

namespace NutriApp;

class GetShoppingListCommand<T> : Command<T>
{
    private App app;

    public GetShoppingListCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(T userinput)
    {
        
    }
}