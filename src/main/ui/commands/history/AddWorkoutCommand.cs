using System;

namespace NutriApp.UI
{
    class AddWorkoutCommand : Command
    {
        public AddWorkoutCommand(App app) : base(app) { }
        public override void Execute<T>(T data)
        {
            throw new NotImplementedException();
        }
    }
}