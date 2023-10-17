using System;
using NutriApp.Food;

namespace NutriApp.UI;

class PTGetShoppingListInvoker : CommandInvoker<ShoppingList>
{
    public PTGetShoppingListInvoker(Command<GetShoppingList> command) : base(command) { }

    public override void Invoke()
    {

    }
}