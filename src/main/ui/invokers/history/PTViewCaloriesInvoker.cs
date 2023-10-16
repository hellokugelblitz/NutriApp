using System;

namespace NutriApp
{
    class PTViewCaloriesInvoker : CommandInvoker
    {
        protected Command command;

        public PTViewCaloriesInvoker(Command command)
        {
            this.command = command;
        }

        public void Invoke()
        {

        }
    }
}