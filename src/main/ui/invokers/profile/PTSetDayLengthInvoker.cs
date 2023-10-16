using System;

namespace NutriApp
{
    class PTSetDayLengthInvoker : CommandInvoker
    {
        protected Command command;

        public PTSetDayLengthInvoker(Command command)
        {
            this.command = command;
        }

        public void Invoke()
        {

        }
    }
}