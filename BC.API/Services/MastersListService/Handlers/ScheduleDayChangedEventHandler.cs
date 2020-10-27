using System.Threading.Tasks;
using BC.API.Events;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

  public class UserAssignedToRoleHandler : IIntegrationEventHandler<UserAssignedToRoleEvent>
  {
    private readonly MastersListService _mastersListService;

    public UserAssignedToRoleHandler(MastersListService mastersListService)
    {
      _mastersListService = mastersListService;
    }

    public async Task Handle(UserAssignedToRoleEvent @event)
    {
      await this._mastersListService.OnUserAssignedToRole(@event);
    }

    public void Dispose()
    {
    }
  }
}
