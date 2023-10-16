using System;

namespace NutriApp
{
    class PTSetWeightInvoker : CommandInvoker
    {
        protected Command command;

        public PTSetWeightInvoker(Command command)
        {
            this.command = command;
        }

        public override void Invoke()
        {

        }
    }
}