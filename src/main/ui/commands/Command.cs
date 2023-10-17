using System;

namespace NutriApp
{
    abstract class Command
    {
        public delegate void CommandFinished();
        protected CommandFinished onFinished;
        protected App _app;
        public Command(App app)
        {
            _app = app;
        }

        public abstract void Execute<T>(T data);
    }

    
}