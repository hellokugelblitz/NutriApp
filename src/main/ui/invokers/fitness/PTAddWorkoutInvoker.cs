using System;

namespace NutriApp
{
    class PTAddWorkoutInvoker : CommandInvoker
    {
        protected Command command;

        public PTAddWorkoutInvoker(Command command)
        {
            this.command = command;
        }

        public override void Invoke()
        {

        }
    }
}