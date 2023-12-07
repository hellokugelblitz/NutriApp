using System.IO;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Save;

namespace NutriApp.Controllers;

[Route("api/Save")]
[ApiController]
[Authorize]
public class SaveApiController : ControllerBase
{
    private readonly App _app;
    
    public SaveApiController(App app)
    {
        _app = app;
    }

    [HttpPut("setsave")]
    public IActionResult SetSaveType([FromQuery] string saveType)
    {
        switch (saveType)
        {
            case "csv":
                _app.SaveSyst.SetFileType(new CSVAdapter());
                break;
            case "json":
                _app.SaveSyst.SetFileType(new JSONAdapter());
                break;
            case "xml":
                _app.SaveSyst.SetFileType(new XMLAdapter());
                break;
            default:
                return BadRequest("that is not a supported filetype");
        }

        return Ok();
    }

    [HttpGet("export/meals")]
    public IActionResult ExportMeals()
    {
        string filePath = "meals." + _app.SaveSyst.GetFileSaver().GetFileType();
        _app.FoodControl.ExportMeals(filePath);
        

        // Read the file into a byte array
        var fileBytes = System.IO.File.ReadAllBytes(filePath);

        // Determine the MIME type of the file
        var contentType = "application/octet-stream"; // You can adjust the content type based on the file type


        // Send the file to the client
        return File(fileBytes, contentType, filePath);
    }

    [HttpPost("import/meals")]
    public IActionResult ImportMeals()
    {
        var file = Request.Form.Files[0]; // Assuming only one file is being uploaded

        if (file.Length > 0)
        {
            var filePath = Path.Combine("wwwroot", "uploads", file.FileName); // Specify the path where you want to save the file

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            
            _app.FoodControl.ImportMeals(filePath);
            System.IO.File.Delete(filePath); //cleanup temp file
            return Ok(new { message = "File uploaded successfully", filePath });
        }
        else
        {
            return BadRequest("File is empty");
        }
    }
    
    [HttpGet("export/recipes")]
    public IActionResult ExportRecipes()
    {
        string filePath = "recipes." + _app.SaveSyst.GetFileSaver().GetFileType();
        _app.FoodControl.ExportRecipes(filePath);
        

        // Read the file into a byte array
        var fileBytes = System.IO.File.ReadAllBytes(filePath);

        // Determine the MIME type of the file
        var contentType = "application/octet-stream"; // You can adjust the content type based on the file type


        // Send the file to the client
        return File(fileBytes, contentType, filePath);
    }

    [HttpPost("import/recipes")]
    public IActionResult ImportRecipes()
    {
        var file = Request.Form.Files[0]; // Assuming only one file is being uploaded

        if (file.Length > 0)
        {
            var filePath = Path.Combine("wwwroot", "uploads", file.FileName); // Specify the path where you want to save the file

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            
            _app.FoodControl.ImportRecipes(filePath);
            System.IO.File.Delete(filePath); //cleanup temp file
            return Ok(new { message = "File uploaded successfully", filePath });
        }
        else
        {
            return BadRequest("File is empty");
        }
    }
}