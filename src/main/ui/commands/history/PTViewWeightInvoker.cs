using System;

namespace NutriApp
{
    class PTViewWeightInvoker : CommandInvoker
    {
        protected Command command;

        public PTViewWeightInvoker(Command command)
        {
            this.command = command;
        }

        public void Invoke()
        {

        }
    }
}