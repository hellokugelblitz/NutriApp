using System;

namespace NutriApp
{
    class SetDayLengthCommand : Command
    {
        private App app;

        public SetDayLengthCommand(App app)
        {
            this.app = app;
        }

        public override void Execute()
        {
            
        }
    }
}