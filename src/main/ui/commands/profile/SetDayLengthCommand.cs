using System;

namespace NutriApp
{
    class SetDayLengthCommand<T> : Command<T>
    {
        private App app;

        public SetDayLengthCommand(App app)
        {
            this.app = app;
        }

        public override void Execute(T userinput)
        {
            
        }
    }
}