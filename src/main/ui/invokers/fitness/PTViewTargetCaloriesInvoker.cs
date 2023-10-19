using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTViewTargetCaloriesInvoker : CommandInvoker<string>
{
	private App app;

	public PTViewTargetCaloriesInvoker(Command<string> command, App app) : base(command) 
	{ 
		this.app = app;
	}

	public override void Invoke()
	{
		command.Execute("");
	}
}