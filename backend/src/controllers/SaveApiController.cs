using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Controllers.Middleware;
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
            var filePath =
                Path.Combine("wwwroot", "uploads", file.FileName); // Specify the path where you want to save the file

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
            var filePath =
                Path.Combine("wwwroot", "uploads", file.FileName); // Specify the path where you want to save the file

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

    [HttpGet("export/user")]
    public IActionResult ExportFolder()
    {
        var user = HttpContext.GetUser();
        string folderName = _app.SaveSyst.CreateNewestFolderName(user.UserName);
        _app.SaveSyst.SaveUser(folderName);

        string zipPath = user.UserName + ".zip";

        // Create the zip file and add the contents of the folder
        ZipFile.CreateFromDirectory(folderName, zipPath);

        // Read the zip file into a byte array
        var zipFileBytes = System.IO.File.ReadAllBytes(zipPath);

        // Determine the MIME type of the file
        var contentType = "application/zip";

        // Specify the file name for the client's download
        var fileName = "exampleFolder.zip";
        System.IO.File.Delete(zipPath);
        // Send the zip file to the client
        return File(zipFileBytes, contentType, fileName);
    }

    [HttpPost("import/user")]
    public IActionResult ImportFolder()
    {
        var folder = Request.Form.Files[0];

        if (folder.Length > 0)
        {
            // Specify the path where you want to save the zip file
            var zipFilePath = Path.Combine("wwwroot", "uploads", $"{Path.GetRandomFileName()}.zip");

            using (var fileStream = new FileStream(zipFilePath, FileMode.Create))
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                // Add all files from the selected folder to the zip archive
                var files = GetAllFiles(folder);
                foreach (var filePath in files)
                {
                    var entryName = Path.GetRelativePath(folder.FileName, filePath);
                    var zipEntry = archive.CreateEntry(entryName);

                    using (var entryStream = zipEntry.Open())
                    using (var fileReadStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        fileReadStream.CopyTo(entryStream);
                    }
                }
            }

            return Ok(new { message = "Folder uploaded successfully", zipFilePath });
        }
        else
        {
            return BadRequest("Folder is empty");
        }
    }
    
    // Helper method to get all files in the selected folder and its subfolders
    private static IEnumerable<string> GetAllFiles(IFormFile folder)
    {
        using (var streamReader = new StreamReader(folder.OpenReadStream()))
        {
            while (streamReader.Peek() >= 0)
            {
                var line = streamReader.ReadLine();
                if (System.IO.File.Exists(line))
                {
                    yield return line;
                }
            }
        }
    }
}