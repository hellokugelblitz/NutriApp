using System;

namespace NutriApp
{
    class PTConsumeMealInvoker : CommandInvoker
    {
        protected Command command;

        public PTConsumeMealInvoker(Command command)
        {
            this.command = command;
        }

        public void Invoke()
        {

        }
    }
}