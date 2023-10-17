using System;

namespace NutriApp;

class ConsumeMealCommand : Command
{
    private App app;

    public ConsumeMealCommand(App app)
    {
        this.app = app;
    }

    public override void Execute()
    {
        
    }
}