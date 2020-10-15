using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BC.API.Services.FilesService.Controllers
{
  [Route("pre")]
  [ApiExplorerSettings(GroupName = "file-service")]
  public class PreController : Controller
  {
    private HttpClient _client;

    public PreController(HttpClient client)
    {
      _client = client;
    }

    [HttpPost]
    public async Task<IActionResult> PostFile([FromQuery] bool isTemporary = false)
    {
      var file = Request.Form.Files[0];

      if (file.Length <= 0)
      {
        return BadRequest();
      }

      var formData = new MultipartFormDataContent();
      formData.Add(new StreamContent(file.OpenReadStream()), file.Name, file.FileName);

      var req = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5207/files") {Content = formData};
      await this._client.SendAsync(req);

      return Ok();
    }
  }
}
