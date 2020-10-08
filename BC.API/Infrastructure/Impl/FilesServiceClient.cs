using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BC.API.Infrastructure.Interfaces;

namespace BC.API.Infrastructure.Impl
{
  public class FilesServiceClient: IFilesServiceClient
  {
    private HttpClient _client;

    public FilesServiceClient(HttpClient client)
    {
      _client = client;
    }

    public async Task<string> PostFile(Stream fileStream)
    {
      var formData = new MultipartFormDataContent();
      formData.Add(new StreamContent(fileStream), "", "");

      var req = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5207/files") {Content = formData};
      var res = await this._client.SendAsync(req);

      return res.Content.ToString();
    }
  }
}
