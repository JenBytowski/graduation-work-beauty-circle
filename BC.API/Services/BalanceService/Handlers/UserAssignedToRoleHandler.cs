using System.Threading.Tasks;
using BC.API.Events;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.BalanceService.Handlers
{
  public class UserAssignedToRoleHandler: IIntegrationEventHandler<UserAssignedToRoleEvent>
  {
    private readonly BalanceService _balanceService;

    public UserAssignedToRoleHandler(BalanceService balanceService)
    {
      _balanceService = balanceService;
    }

    public async Task Handle(UserAssignedToRoleEvent @event)
    {
      await this._balanceService.OnUserAssignedToRole(@event);
    }

    public void Dispose()
    {

    }
  }
}
