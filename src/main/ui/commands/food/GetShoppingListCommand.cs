using System;

namespace NutriApp;

class GetShoppingListCommand : Command
{
    private App app;

    public GetShoppingListCommand(App app)
    {
        this.app = app;
    }

    public override void Execute()
    {
        
    }
}