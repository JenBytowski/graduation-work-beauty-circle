using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace BC.API.Services.FilesService.Controllers
{
  [Route("files")]
  [ApiExplorerSettings(GroupName = "file-service")]
  public class FilesController : Controller
  {
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly string _filesFolder;

    
    
    private string GenerateFilePatch(string name)
    {
      var fileNameSegments = name.Split(new[] {'/', '\\'});
      return Path.Combine(new []{this._filesFolder}.Union(fileNameSegments).ToArray());
    }

    public FilesController(IWebHostEnvironment appEnvironment)
    {
      _appEnvironment = appEnvironment;
      _filesFolder = Path.Combine(_appEnvironment.ContentRootPath, @"Data\FilesFolder"); // TODO: Вынести в конфигурацию
    }

    [HttpGet]
    [Route("{*name}")]
    public IActionResult GetFile([FromRoute] string name)
    {
      var filePath = GenerateFilePatch(name);

      new FileExtensionContentTypeProvider().TryGetContentType(name, out var contentType);
      return PhysicalFile(filePath, contentType);
    }

    [HttpPost]
    public IActionResult PostFile([FromQuery] bool isTemporary = false)
    {
      try
      {
        var file = Request.Form.Files[0];

        if (file.Length <= 0)
        {
          return BadRequest();
        }

        var fileName = Request.Form.Files[0].FileName;
        var filePatch = GenerateFilePatch(fileName);
        
        var dir = Path.GetDirectoryName(filePatch);
        if (!Directory.Exists(dir))
        {
          Directory.CreateDirectory(dir);  
        }

        using (var stream = new FileStream(filePatch, FileMode.Create))
        {
          file.CopyTo(stream);
        }

        return Ok();
      }
      
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex}");
      }
    }

    [HttpDelete]
    [Route("{name}")]
    public IActionResult DeleteFile([FromRoute] string name)
    {
      var filePath = Path.Combine(_filesFolder, name);
      
      if (!System.IO.File.Exists(filePath))
      {
        return BadRequest(new BadAPIResponse
        {
          Messages = new []
          {
            $"File {name} is already deleted or not exist"
          }
        });
      }
      
      System.IO.File.Delete(filePath);
      
      return Ok();
    }
  }
}
