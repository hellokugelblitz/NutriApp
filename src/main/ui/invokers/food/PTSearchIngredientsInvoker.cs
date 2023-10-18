using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTSearchIngredientsInvoker : CommandInvoker<string>
{
	private App app;

	public PTSearchIngredientsInvoker(Command<string> command, App app) : base(command) 
	{ 
		this.app = app;
	}

	public override void Invoke()
	{
		Console.WriteLine("Enter name of food to search: ");
		string name = Console.ReadLine();

		command.Execute(name);
	}
}