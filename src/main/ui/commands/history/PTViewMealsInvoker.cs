using System;

namespace NutriApp
{
    class PTViewMealsInvoker : CommandInvoker
    {
        protected Command command;

        public PTViewMealsInvoker(Command command)
        {
            this.command = command;
        }

        public void Invoke()
        {

        }
    }
}