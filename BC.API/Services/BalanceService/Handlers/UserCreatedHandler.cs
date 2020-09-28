using System.Threading.Tasks;
using BC.API.Events;
using BC.API.Services.BalanceService.Data;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.BalanceService.Handlers
{
  public class UserCreatedHandler: IIntegrationEventHandler<UserCreatedEvent>
  {
    private BalanceContext _context;
    private BalanceService _balanceService;

    public UserCreatedHandler(BalanceContext context, BalanceService balanceService)
    {
      _context = context;
      _balanceService = balanceService;
    }

    public async Task Handle(UserCreatedEvent @event)
    {
      await this._balanceService.OnUserCreated(new Events.UserCreatedEvent()
      {
        UserId = @event.UserId,
        UserName = @event.UserName,
        Role = @event.Role
      });
    }

    public void Dispose()
    {

    }
  }
}
