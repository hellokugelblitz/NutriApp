using System;

namespace NutriApp
{
    class PTPurchaseFoodInvoker : CommandInvoker
    {
        protected Command command;

        public PTPurchaseFoodInvoker(Command command)
        {
            this.command = command;
        }

        public override void Invoke()
        {

        }
    }
}