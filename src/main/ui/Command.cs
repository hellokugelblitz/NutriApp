using System;

namespace NutriApp
{
    abstract class Command
    {

        // protected CommandFinished onFinished;

        public abstract void Execute();
    }
}