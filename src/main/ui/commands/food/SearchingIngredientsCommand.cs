using System;

namespace NutriApp
{
    class SearchingIngredientsCommand<T> : Command<T>
    {
        private App app;

        public SearchingIngredientsCommand(App app)
        {
            this.app = app;
        }

        public override void Execute(T userinput)
        {
            
        }
    }
}