using System;

namespace NutriApp
{
    class ViewCaloriesCommand<T> : Command<T>
    {
        private App app;

        public ViewCaloriesCommand(App app)
        {
            this.app = app;
        }

        public override void Execute(T userinput)
        {
            
        }
    }
}