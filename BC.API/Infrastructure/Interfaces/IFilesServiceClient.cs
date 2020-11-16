using System.IO;
using System.Threading.Tasks;

namespace BC.API.Infrastructure.Interfaces
{
  public interface IFilesServiceClient
  {
    Task<Stream> GetFile(string name);

    Task PostFile(Stream fileStream, string fileName);

    Task DeleteFile(string name);
  }
}
