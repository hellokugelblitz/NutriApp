using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTViewTargetCaloriesInvoker : CommandInvoker<string>
{

	public PTViewTargetCaloriesInvoker(Command<string> command) : base(command) { }

	public override void Invoke()
	{
		command.Execute("");
	}
}