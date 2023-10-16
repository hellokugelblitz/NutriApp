using System;

namespace NutriApp
{
    class PTGetShoppingListInvoker : CommandInvoker
    {
        protected Command command;

        public PTGetShoppingListInvoker(Command command)
        {
            this.command = command;
        }

        public override void Invoke()
        {

        }
    }
}