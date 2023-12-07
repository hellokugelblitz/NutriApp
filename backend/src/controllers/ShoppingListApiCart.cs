using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Middleware;
using NutriApp.Controllers.Models;

namespace NutriApp.Controllers;

[Route("api/ShoppingList")]
[ApiController]
[Authorize]
public class ShoppingListApiCart : ControllerBase
{
    private readonly App _app;
    
    public ShoppingListApiCart(App app)
    {
        _app = app;
    }
    
    // GET api/ShoppingList
    [HttpGet]
    public ActionResult<IEnumerable<ShoppingListEntryModel>> GetShoppingList()
    {
        var user = HttpContext.GetUser();
        return _app.ShoppingListControl.GetShoppingList(user.Name)
            .Entries.Select(ShoppingListEntryModel.FromEntry).ToList();
    }
}