using System;

namespace NutriApp.UI
{
    abstract class Command<T>
    {

        protected CommandFinished onFinished;

        public void Subscribe(CommandFinished handler)
        {
            onFinished += handler;
        }

        public abstract void Execute(T userinput);
    }

    delegate void CommandFinished();
}