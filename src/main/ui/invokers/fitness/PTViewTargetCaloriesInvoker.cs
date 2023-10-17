using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTViewTargetCaloriesInvoker : CommandInvoker<Goal.Goal>
{
	private App app;

	public PTViewTargetCaloriesInvoker(Command<Goal.Goal> command, App app) : base(command) 
	{ 
		this.app = app;
	}

	public override void Invoke()
	{

	}
}