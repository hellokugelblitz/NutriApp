using System;

namespace NutriApp;

class AddWorkoutCommand : Command 
{
    private App app;

    public AddWorkoutCommand(App app)
    {
        this.app = app;
    }

    public override void Execute()
    {
        
    }
}