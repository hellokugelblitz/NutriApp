using System;

namespace NutriApp
{
    class PTSetFitnessGoalInvoker : CommandInvoker
    {
        protected Command command;

        public PTSetFitnessGoalInvoker(Command command)
        {
            this.command = command;
        }

        public override void Invoke()
        {

        }
    }
}