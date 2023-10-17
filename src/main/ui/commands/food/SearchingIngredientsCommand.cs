using System;

namespace NutriApp
{
    class SearchingIngredientsCommand : Command
    {
        private App app;

        public SearchingIngredientsCommand(App app)
        {
            this.app = app;
        }

        public override void Execute()
        {
            
        }
    }
}