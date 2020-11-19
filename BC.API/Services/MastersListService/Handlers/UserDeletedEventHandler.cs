using System;
using System.Linq;
using System.Threading.Tasks;
using BC.API.Domain;
using BC.API.Services.AuthenticationService.Data;
using Microsoft.AspNetCore.Identity;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.MastersListService.Handlers
{
  public class UserDeletedEventHandler : IIntegrationEventHandler<UserDeletedEvent>
  {
    private AuthenticationContext _authenticationContext;
    private MastersListService  _mastersListService;
    
    public UserDeletedEventHandler(AuthenticationContext authenticationContext, MastersListService  mastersListService)
    {
      _authenticationContext = authenticationContext;
      _mastersListService = mastersListService;
    }
    public async Task Handle(UserDeletedEvent @event)
    {
      var user = _authenticationContext.Users.Single(usr => usr.Id == @event.UserId);

      if (_authenticationContext.UserRoles.Contains(new IdentityUserRole<Guid>
      {
        UserId = user.Id, RoleId = _authenticationContext.Roles.Single(role => role.Name == UserRoles.Master).Id
      }))
      {
        var result = await _mastersListService.UnPublishMaster(user.Id);

        if (!result.Result)
        {
          throw new UserDeletedException($"Cant delete master with id: {user.Id}");
        }
        
        return;
      }

      _authenticationContext.Users.Remove(user);
      await _authenticationContext.SaveChangesAsync();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }

  public class UserDeletedException : ApplicationException
  {
    public UserDeletedException(string message) : base(message)
    {}
  }
  
  public class UserDeletedEvent : IntegrationEvent
  {
    public Guid UserId { get; set; }
  }
}
