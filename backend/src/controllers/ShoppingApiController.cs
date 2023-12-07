using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Food;

namespace NutriApp.Controllers;

[Route("api/Shopping")]

[ApiController]
public class ShoppingApiController : ControllerBase
{
    private readonly App _app;

    public ShoppingApiController(App app)
    {
        _app = app;
    }
}