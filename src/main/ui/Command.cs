using System;

namespace NutriApp
{
    abstract class Command<T>
    {

        protected CommandFinished onFinished;

        public abstract void Execute(T userinput);
    }

    delegate void CommandFinished();
}