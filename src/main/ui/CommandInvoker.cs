using System;

namespace NutriApp
{
    abstract class CommandInvoker
    {
        protected Command command;

        public CommandInvoker(Command command)
        {
            this.command = command;
        }

        public abstract void Invoke();
    }
}