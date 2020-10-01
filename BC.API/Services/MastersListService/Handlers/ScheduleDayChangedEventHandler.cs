using System.Threading.Tasks;
using BC.API.Events;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.MastersListService.Handlers
{
  public class ScheduleDayChangedEventHandler : IIntegrationEventHandler<ScheduleDayChangedEvent>
  {
    private MastersListService _mastersListService;

    public ScheduleDayChangedEventHandler(MastersListService mastersListService)
    {
      _mastersListService = mastersListService;
    }

    public async Task Handle(ScheduleDayChangedEvent @event)
    {
      this._mastersListService.OnScheduleDayChanged(@event);
    }

    public void Dispose()
    {
      throw new System.NotImplementedException();
    }
  }
}
