using System;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Events
{
  public class UserCreatedEvent: IntegrationEvent
  {
    public Guid UserId { get; set; }
    public string UserName { get; set; }
  }

  public class UserAssignedToRoleEvent : IntegrationEvent
  {
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }
  }
}
