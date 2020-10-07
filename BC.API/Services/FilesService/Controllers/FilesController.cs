using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace BC.API.Services.FilesService.Controllers
{
  [Route("files")]
  public class FilesController : Controller
  {
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly string _filesFolder;

    private string GenerateFilePatch(string name)
    {
      return Path.Combine(this._filesFolder, name);
    }

    public FilesController(IWebHostEnvironment appEnvironment)
    {
      _appEnvironment = appEnvironment;
      _filesFolder = Path.Combine(_appEnvironment.ContentRootPath, "FilesFolder"); // TODO: Вынести в конфигурацию
    }

    [HttpGet]
    [Route("{name}")]
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

        var fileName = Guid.NewGuid() + Path.GetExtension(Request.Form.Files[0].FileName);
        var fullPath = GenerateFilePatch(fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
          file.CopyTo(stream);
        }

        return Ok(fileName);
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
      return Ok();
    }
  }
}
