using System;

namespace NutriApp;

class ConsumeMealCommand<T> : Command<T>
{
    private App app;

    public ConsumeMealCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(T userinput)
    {
        
    }
}