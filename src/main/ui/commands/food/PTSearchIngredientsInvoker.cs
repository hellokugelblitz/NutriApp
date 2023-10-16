using System;

namespace NutriApp
{
	class PTSearchIngredientsInvoker : CommandInvoker
	{
		protected Command command;

		public PTSearchIngredientsInvoker(Command command)
		{
			this.command = command;
		}

		public void Invoke()
		{

		}
	}
}