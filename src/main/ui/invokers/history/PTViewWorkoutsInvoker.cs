using System;

namespace NutriApp
{
    class PTViewWorkoutsInvoker : CommandInvoker
    {
        protected Command command;

        public PTViewWorkoutsInvoker(Command command)
        {
            this.command = command;
        }

        public void Invoke()
        {

        }
    }
}