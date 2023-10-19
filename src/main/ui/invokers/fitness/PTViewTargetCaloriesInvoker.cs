using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTViewTargetCaloriesInvoker : CommandInvoker<None>
{

	public PTViewTargetCaloriesInvoker(Command<None> command) : base(command) { }

	public override void Invoke()
	{
		command.Execute(null);
	}
}