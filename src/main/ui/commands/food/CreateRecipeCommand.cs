using System;

namespace NutriApp;

class CreateRecipeCommand<T> : Command<T>
{
    private App app;

    public CreateRecipeCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(T userinput)
    {
        
    }
}