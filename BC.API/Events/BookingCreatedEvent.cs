using System;

namespace BC.API.Events
{
  public class BookingCreatedEvent
  {
    public Guid ClientId { get; set; }
    public Guid MasterId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
  }
}
