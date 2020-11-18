using System.Threading.Tasks;
using BC.API.Events;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.BookingService.Handlers
{
  public class UserAssignedToRoleHandler: IIntegrationEventHandler<UserAssignedToRoleEvent>
  {
    private readonly BookingService _bookingService;

    public UserAssignedToRoleHandler(BookingService bookingService)
    {
      _bookingService = bookingService;
    }

    public async Task Handle(UserAssignedToRoleEvent @event)
    {
      await this._bookingService.OnUserAssignedToRole(@event);
    }

    public void Dispose()
    {

    }
  }
}
