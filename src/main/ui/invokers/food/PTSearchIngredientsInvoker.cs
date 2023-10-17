using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTSearchIngredientsInvoker : CommandInvoker<Ingredient>
{
	private App app;

	public PTSearchIngredientsInvoker(Command<Ingredient> command, App app) : base(command) 
	{ 
		this.app = app;
	}

	public override void Invoke()
	{

	}
}