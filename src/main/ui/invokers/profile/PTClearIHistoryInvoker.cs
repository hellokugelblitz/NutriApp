using System;

namespace NutriApp.UI
{
    class PTClearHistoryInvoker : CommandInvoker
    {
        protected Command command;

        public PTClearHistoryInvoker(Command command): base(command) { }

        public override void Invoke()
        {

        }
    }
}