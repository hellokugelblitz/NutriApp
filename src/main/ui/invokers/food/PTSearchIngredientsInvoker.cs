using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTSearchIngredientsInvoker : CommandInvoker<Ingredient>
{
	public PTSearchIngredientsInvoker(Command<Ingredient> command) : base(command) { }

	public override void Invoke()
	{

	}
}