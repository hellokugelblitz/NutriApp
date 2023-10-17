using System;

namespace NutriApp;

class CreateRecipeCommand : Command
{
    private App app;

    public CreateRecipeCommand(App app)
    {
        this.app = app;
    }

    public override void Execute()
    {
        
    }
}