using System;
using System.Collections.ObjectModel;

namespace NutriApp.UI
{
    abstract class CommandInvoker<T> : Invoker
    {
        protected Command<T> command;

        public CommandInvoker(Command<T> command)
        {
            this.command = command;
        }

        public abstract void Invoke();
    }
}