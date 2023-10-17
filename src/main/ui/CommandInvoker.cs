using System;

namespace NutriApp
{
    abstract class CommandInvoker<T>
    {
        protected Command<T> command;

        public CommandInvoker(Command<T> command)
        {
            this.command = command;
        }

        public abstract void Invoke();
    }
}