using System;

namespace NutriApp
{
    class PTCreateRecipesInvoker : CommandInvoker
    {
        protected Command command;

        public PTCreateRecipesInvoker(Command command)
        {
            this.command = command;
        }

        public override void Invoke()
        {

        }
    }
}