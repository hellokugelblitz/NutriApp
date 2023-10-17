using System;

namespace NutriApp;

class AddWorkoutCommand<T> : Command<T>
{
    private App app;

    public AddWorkoutCommand(App app)
    {
        this.app = app;
    }

    public override void Execute(T userinput)
    {
        
    }
}