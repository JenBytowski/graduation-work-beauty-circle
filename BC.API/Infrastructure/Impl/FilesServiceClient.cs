using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BC.API.Infrastructure.Interfaces;

namespace BC.API.Infrastructure.Impl
{
  public class FilesServiceClient: IFilesServiceClient
  {
    private HttpClient _httpClient;
    private FilesServiceClientConfig _config;

    public FilesServiceClient(HttpClient httpClient, FilesServiceClientConfig config)
    {
      _httpClient = httpClient;
      _config = config;
    }

    public async Task<string> PostFile(Stream fileStream)
    {
      var formData = new MultipartFormDataContent();
      formData.Add(new StreamContent(fileStream), "file", "");

      var req = new HttpRequestMessage(HttpMethod.Post, _config.FilesServicePublicUrl) {Content = formData};
      var res = await this._httpClient.SendAsync(req);

      if (!res.IsSuccessStatusCode)
      {
        throw new ApplicationException("Cant post file to File Service");
      }
      
      return res.Content.ToString();
    }
  }

  public class FilesServiceClientConfig
  {
    public string FilesServiceInternalUrl { get; set; }
    
    public string FilesServicePublicUrl { get; set; }
  }
}
