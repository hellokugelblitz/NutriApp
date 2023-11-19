using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// Accessible by http://localhost:5072/hello-world
[ApiController]
[Route("hello-world")]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(new Dictionary<string, string>() { {"message", "Hello world!"} });
    }
}