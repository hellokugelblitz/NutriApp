using System;

namespace NutriApp.Undo;

public class UndoPurchaseFood : UndoCommand
{
    private App _app;
    private User _user;

    public UndoPurchaseFood(App app, User user)
    {
        _app = app;
        _user = user;
    }

    public override void Execute()
    {
        var lastPurchasedItems = _app.FoodControl.GetAllIngredientStocks(_user.Name);
        foreach (var item in lastPurchasedItems.stocks) 
        {
            string ingredientName = item.Key;
            double quantityPurchased = item.Value;

            _app.FoodControl.EditIngredientStock(ingredientName, -quantityPurchased, _user.UserName);
        }

        onFinished?.Invoke();
    }
}