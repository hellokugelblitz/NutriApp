using System;

namespace NutriApp
{
    class PTClearHistoryInvoker : CommandInvoker
    {
        protected Command command;

        public PTClearHistoryInvoker(Command command)
        {
            this.command = command;
        }

        public void Invoke()
        {

        }
    }
}