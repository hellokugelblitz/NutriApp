using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTViewTargetCaloriesInvoker : CommandInvoker<Workout.Goal>
{
	private App app;

	public PTViewTargetCaloriesInvoker(Command<Workout.Goal> command, App app) : base(command) 
	{ 
		this.app = app;
	}

	public override void Invoke()
	{

	}
}