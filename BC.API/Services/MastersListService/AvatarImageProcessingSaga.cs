using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BC.API.Infrastructure.Impl;
using BC.API.Services.MastersListService.Data;
using Microsoft.EntityFrameworkCore;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.MastersListService
{
  public class AvatarImageProcessingSaga : IIntegrationEventHandler<AvatarImageProcessingSaga.SagaEvent>
  {
    public class SagaEvent : IntegrationEvent
    {
      public int Step { get; set; } = 0;
      public Guid MasterId { get; set; }
    }

    private readonly IEventBus _eventBus;
    private readonly MastersContext _context;
    private readonly FilesServiceClient _filesServiceClient;
    private readonly HttpClient _httpClient;

    public AvatarImageProcessingSaga(IEventBus eventBus, MastersContext context, FilesServiceClient filesServiceClient,
      HttpClient httpClient)
    {
      _eventBus = eventBus;
      _context = context;
      _filesServiceClient = filesServiceClient;
      _httpClient = httpClient;
    }

    public void Start(Guid masterId)
    {
      this._eventBus.Publish(new SagaEvent {MasterId = masterId});
    }

    public async Task Handle(SagaEvent @event)
    {
      switch (@event.Step)
      {
        case 0:
          await this.CreateAvatar(@event.MasterId);
          break;
        case 1:
          await this.CreateThumbnail(@event.MasterId);
          break;
        default:
          return;
      }

      @event.Step++;
      this._eventBus.Publish(@event);
    }

    private async Task CreateAvatar(Guid masterId)
    {
      try
      {
        var master = await this._context.Masters.SingleAsync(m => m.Id == masterId);

        var masterSourcePic =
          await (await _httpClient.GetAsync(_filesServiceClient.FilesServiceUrl + master.AvatarSourceFileName))
            .Content
            .ReadAsStreamAsync();

        var processedImageStream = ProcessImage(masterSourcePic, 600, 600);
        var avatarFileName = master.AvatarSourceFileName.Replace("avatarSource", "avatar");

        await _filesServiceClient.PostFile(processedImageStream, avatarFileName);

        master.AvatarFileName = avatarFileName;
        await this._context.SaveChangesAsync();
      }
      catch (CantPostFileToFilesServiceException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private async Task CreateThumbnail(Guid masterId)
    {
      try
      {
        var master = await this._context.Masters.SingleAsync(m => m.Id == masterId);

        var masterSourcePic =
          await (await _httpClient.GetAsync(_filesServiceClient.FilesServiceUrl + master.AvatarSourceFileName))
            .Content
            .ReadAsStreamAsync();

        var processedImageStream = ProcessImage(masterSourcePic, 160, 160);
        var thumbnailFileName = master.AvatarSourceFileName.Replace("avatarSource", "thumbnail");

        await _filesServiceClient.PostFile(processedImageStream, thumbnailFileName);

        master.ThumbnailFileName = thumbnailFileName;
        await this._context.SaveChangesAsync();
      }
      catch (CantPostFileToFilesServiceException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private Stream ProcessImage(Stream imageStream, int width, int height)
    {
      var sourceImage = Image.FromStream(imageStream) as Bitmap;
      var resultImage = sourceImage.GetThumbnailImage(width, height, null, IntPtr.Zero);

      var resultImageStream = new MemoryStream();
      resultImage.Save(resultImageStream,
        ImageCodecInfo.GetImageEncoders().Single(x => x.FormatDescription == nameof(ImageFormat.Jpeg).ToUpper()),
        new EncoderParameters {Param = new[] {new EncoderParameter(Encoder.Quality, 75L)}});

      return resultImageStream;
    }

    public void Dispose()
    {
    }
  }
}
