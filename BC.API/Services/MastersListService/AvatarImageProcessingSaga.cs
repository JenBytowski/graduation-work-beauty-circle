using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using BC.API.Services.MastersListService.Data;
using Microsoft.EntityFrameworkCore;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.MastersListService
{
  public class AvatarImageProcessingSaga: IIntegrationEventHandler<AvatarImageProcessingSaga.SagaEvent>
  {
    public class SagaEvent : IntegrationEvent
    {
      public int Step { get; set; } = 0;
      public Guid MasterId { get; set; }
    }
    
    private readonly IEventBus _eventBus;
    private readonly MastersContext _context;
    private readonly MastersListServiceConfig _config;

    public AvatarImageProcessingSaga(IEventBus eventBus, MastersContext context, MastersListServiceConfig config)
    {
      _eventBus = eventBus;
      _context = context;
      _config = config;
    }

    public void Start(Guid masterId)
    {
      this._eventBus.Publish(new SagaEvent { MasterId = masterId});
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
      master.AvatarFileName = master.AvatarSourceFileName;
      await this._context.SaveChangesAsync();
      Console.WriteLine("Create Avatar");
    }

    private async Task CreateThumbnail(Guid masterId)
    {
      var master = await this._context.Masters.SingleAsync(m => m.Id == masterId);
      master.ThumbnailFileName = master.AvatarSourceFileName;
      await this._context.SaveChangesAsync();
      Console.WriteLine("Create Thumbnail");
    }

    private Bitmap ProcessImage(Stream imageStream, int sideLength)
    {
      var image = (Bitmap)Image.FromStream(imageStream);
      
      if (image.Width / image.Height != 1)
      {
        image = CropImageToSquare(image);
      }

      return ResizeImage(image, new Size {Width = sideLength, Height = sideLength});
    }

    private Bitmap CropImageToSquare(Bitmap image)
    {
      var delta = Math.Abs(image.Width - image.Height);

      if (image.Width > image.Height)
      {
        return image.Clone(new Rectangle(delta / 2, 0, image.Width - delta, image.Height),
          image.PixelFormat);
      }

      return image.Clone(new Rectangle(0, delta / 2, image.Width, image.Height - delta), image.PixelFormat);
    }
    
    private Bitmap ResizeImage(Image imgToResize, Size size)
    {
      var sourceWidth = imgToResize.Width;
      var sourceHeight = imgToResize.Height;

      var nPercent = 0f;
      var nPercentW = 0f;
      var nPercentH = 0f;

      nPercentW = (float)size.Width / sourceWidth;
      nPercentH = (float)size.Height / sourceHeight;

      if (nPercentH < nPercentW)
      {
        nPercent = nPercentH;
      }
      else
      {
        nPercent = nPercentW;
      }

      var destWidth = (int)(sourceWidth * nPercent);
      var destHeight = (int)(sourceHeight * nPercent);

      var result = new Bitmap(destWidth, destHeight);
      Graphics graphics = Graphics.FromImage(result);

      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
      graphics.Dispose();

      return result;
    }

    public void Dispose()
    { }
  }
}
