using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BC.API.Infrastructure.Interfaces;

namespace BC.API.Infrastructure.Impl
{
  public class FilesServiceClient : IFilesServiceClient
  {
    private HttpClient _httpClient;
    private FilesServiceClientConfig _config;

    public FilesServiceClient(HttpClient httpClient, FilesServiceClientConfig config)
    {
      FilesServiceUrl = config.FilesServiceInternalUrl;

      _httpClient = httpClient;
      _config = config;
    }

    public string FilesServiceUrl { get; private set; }

    public async Task<Stream> GetFile(string name)
    {
      var response = await _httpClient.GetAsync(Path.Combine(_config.FilesServiceInternalUrl,name));

      if (!response.IsSuccessStatusCode)
      {
        throw new CantCallToFilesServiceException("Cant get file from File Service");
      }

      return await response.Content.ReadAsStreamAsync();
    }

    public async Task PostFile(Stream fileStream, string fileName)
    {
      var formData = new MultipartFormDataContent();
      formData.Add(new StreamContent(fileStream), "file", fileName);

      var req = new HttpRequestMessage(HttpMethod.Post, _config.FilesServiceInternalUrl) {Content = formData};
      var res = await this._httpClient.SendAsync(req);

      if (!res.IsSuccessStatusCode)
      {
        throw new CantCallToFilesServiceException("Cant post file to File Service");
      }
    }

    public async Task DeleteFile(string name)
    {
      var response = await _httpClient.DeleteAsync(Path.Combine(_config.FilesServiceInternalUrl, name));

      if (!response.IsSuccessStatusCode)
      {
        throw new CantCallToFilesServiceException("Cant delete file from File Service");
      }
    }


    public string BuildPublicUrl(string fileName)
    {
      return new Uri(new Uri(_config.FilesServicePublicUrl), fileName).ToString();
    }
  }

  public class FilesServiceClientConfig
  {
    public string FilesServiceInternalUrl { get; set; }

    public string FilesServicePublicUrl { get; set; }
  }

  public class CantCallToFilesServiceException : ApplicationException
  {
    public CantCallToFilesServiceException(string message) : base(message)
    {
    }
  }
}
