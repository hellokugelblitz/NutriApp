using System;

namespace NutriApp
{
    class PurchaseFoodCommand : Command
    {
        private App app;

        public PurchaseFoodCommand(App app)
        {
            this.app = app;
        }

        public override void Execute()
        {
            
        }
    }
}