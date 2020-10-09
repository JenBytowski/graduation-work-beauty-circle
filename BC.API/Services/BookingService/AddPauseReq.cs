using System;

namespace BC.API.Services.BookingService
{
  public class AddPauseReq
  {
    public Guid MasterId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Description { get; set; }
  }
}
