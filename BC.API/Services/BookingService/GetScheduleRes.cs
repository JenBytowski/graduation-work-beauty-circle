using System.Collections.Generic;
using BC.API.Services.BookingService.Data;

namespace BC.API.Services.BookingService
{
  public class GetScheduleRes
  {
    public IEnumerable<ScheduleDay> Days { get; set; }
  }
}
