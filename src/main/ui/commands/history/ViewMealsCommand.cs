using System;

namespace NutriApp
{
    class ViewMealsCommand<T> : Command<T>
    {
        private App app;

        public ViewMealsCommand(App app)
        {
            this.app = app;
        }

        public override void Execute(T userinput)
        {
            
        }
    }
}