using System;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Events
{
  public class BalanceChangedEvent : IntegrationEvent
  {
    public Guid HolderId { get; set; }
    public int Balance { get; set; }
  }
}
