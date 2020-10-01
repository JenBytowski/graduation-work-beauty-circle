using System;
using System.Collections.Generic;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Events
{
  public class ScheduleDayChangedEvent : IntegrationEvent
  {
    public Guid MasterId { get; set; }
  }
}
