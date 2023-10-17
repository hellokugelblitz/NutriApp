using System;

namespace NutriApp
{
    class ClearHistoryCommand<T> : Command<T>
    {
        private App app;

        public ClearHistoryCommand(App app)
        {
            this.app = app;
        }

        public override void Execute(T userinput)
        {
            
        }
    }
}