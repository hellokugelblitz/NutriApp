using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTSearchIngredientsInvoker : CommandInvoker<string>
{

	public PTSearchIngredientsInvoker(Command<string> command) : base(command) { }

	public override void Invoke()
	{
		Console.WriteLine("Enter name of food to search: ");
		string name = Console.ReadLine();

		command.Execute(name);
	}
}