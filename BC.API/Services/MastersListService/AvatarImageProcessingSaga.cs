using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
    private readonly MastersListServiceConfig _config;
    private readonly HttpClient _httpClient;

    public AvatarImageProcessingSaga(IEventBus eventBus, MastersContext context, MastersListServiceConfig config,
      HttpClient httpClient)
    {
      _eventBus = eventBus;
      _context = context;
      _config = config;
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
      var master = await this._context.Masters.SingleAsync(m => m.Id == masterId);

      var masterSourcePic =
        await (await _httpClient.GetAsync(_config.FilesServiceInternalUrl + master.AvatarSourceFileName))
          .Content
          .ReadAsStreamAsync();

      var processedImageStream = ProcessImage(masterSourcePic, 600, 600);
      var avatarFileName = master.AvatarSourceFileName.Replace("avatarSource", "avatar");

      var formData = new MultipartFormDataContent();
      formData.Add(new StreamContent(processedImageStream), avatarFileName, avatarFileName);

      var saveFileRequest = new HttpRequestMessage(HttpMethod.Post, _config.FilesServiceInternalUrl) {Content = formData};
      var saveFileResponse = await _httpClient.SendAsync(saveFileRequest);

      if (!saveFileResponse.IsSuccessStatusCode)
      {
        throw new ApplicationException("Cant save master avatar pic");
      }

      master.AvatarFileName = avatarFileName;
      await this._context.SaveChangesAsync();
    }

    private async Task CreateThumbnail(Guid masterId)
    {
      var master = await this._context.Masters.SingleAsync(m => m.Id == masterId);

      var masterSourcePic =
        await (await _httpClient.GetAsync(_config.FilesServiceInternalUrl + master.AvatarSourceFileName))
          .Content
          .ReadAsStreamAsync();
      
      var processedImageStream = ProcessImage(masterSourcePic, 160, 160);
      var thumbnailFileName = master.AvatarSourceFileName.Replace("avatarSource", "thumbnail");

      var formData = new MultipartFormDataContent();
      formData.Add(new StreamContent(processedImageStream), thumbnailFileName, thumbnailFileName);

      var saveFileRequest = new HttpRequestMessage(HttpMethod.Post, _config.FilesServiceInternalUrl) {Content = formData};
      var saveFileResponse = await _httpClient.SendAsync(saveFileRequest);

      if (!saveFileResponse.IsSuccessStatusCode)
      {
        throw new ApplicationException("Cant save master thumbnail pic");
      }

      master.ThumbnailFileName = thumbnailFileName;
      await this._context.SaveChangesAsync();
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
