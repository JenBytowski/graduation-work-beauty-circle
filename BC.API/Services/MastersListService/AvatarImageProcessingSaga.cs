using System;
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

    public AvatarImageProcessingSaga(IEventBus eventBus, MastersContext context)
    {
      _eventBus = eventBus;
      _context = context;
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

    public void Dispose()
    { }
  }
}
