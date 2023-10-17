using System;

namespace NutriApp
{
    class ClearHistoryCommand : Command
    {
        private App app;

        public ClearHistoryCommand(App app)
        {
            this.app = app;
        }

        public override void Execute()
        {
            
        }
    }
}