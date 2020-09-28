using System;
using System.Linq;
using System.Threading.Tasks;
using BC.API.Domain;
using BC.API.Events;
using BC.API.Services.BalanceService.Data;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.BalanceService
{
  public class BalanceService
  {
    private BalanceContext _context;
    private IEventBus _eventBus;

    public BalanceService(BalanceContext context, IEventBus eventBus)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _eventBus = eventBus;
    }

    public async Task OnUserCreated(UserCreatedEvent @event)
    {
      if (@event.Role != UserRoles.Master)
      {
        return;
      }

      var account = await this._context.Accounts.AddAsync(new Account {Id = Guid.NewGuid(), HolderId = @event.UserId, Balance = 15});
      await this._context.SaveChangesAsync();
      this._eventBus.Publish(new BalanceChangedEvent { HolderId = account.Entity.HolderId, Balance = account.Entity.Balance });
    }

    public async Task OnBookingCreated(BookingCreatedEvent @event)
    {
      var account = this._context.Accounts.Single(acc => acc.HolderId == @event.MasterId);
      account.Balance--;
      await this._context.SaveChangesAsync();
      this._eventBus.Publish(new BalanceChangedEvent { HolderId = account.HolderId, Balance = account.Balance });
    }

    public async Task IncreaseBalance(Guid holderId, int amount)
    {
      var account = this._context.Accounts.Single(acc => acc.HolderId == holderId);
      account.Balance += amount;
      await this._context.SaveChangesAsync();
      this._eventBus.Publish(new BalanceChangedEvent { HolderId = account.HolderId, Balance = account.Balance });
    }
  }
}
