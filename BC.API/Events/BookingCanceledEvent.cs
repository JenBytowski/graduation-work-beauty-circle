using System;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Events
{
  public class BookingCanceledEvent: IntegrationEvent
  {
    public Guid BookingId { get; set; }
  }
}
