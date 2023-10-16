using System;

namespace NutriApp
{
	class PTViewTargetCaloriesInvoker : CommandInvoker
	{
		protected Command command;

		public PTViewTargetCaloriesInvoker(Command command)
		{
			this.command = command;
		}

		public void Invoke()
		{

		}
	}
}