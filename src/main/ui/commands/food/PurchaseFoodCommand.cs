using System;

namespace NutriApp
{
    class PurchaseFoodCommand<T> : Command<T>
    {
        private App app;

        public PurchaseFoodCommand(App app)
        {
            this.app = app;
        }

        public override void Execute(T userinput)
        {
            
        }
    }
}