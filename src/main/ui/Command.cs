using System;

namespace NutriApp
{
    abstract class Command<T>
    {

        protected event Action onFinished;

        public void Subscribe(Action handler)
        {
            this.onFinished += handler;
        }

        public abstract void Execute(T userinput);
    }
}