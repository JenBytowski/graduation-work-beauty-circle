using System;

namespace BC.API.Services.BookingService
{
  public class AddBookingReq
  {
    public Guid MasterId { get; set; }

    public Guid ClientId { get; set; }

    public Guid ServiceType { get; set; }

    public string Description { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
  }
}
