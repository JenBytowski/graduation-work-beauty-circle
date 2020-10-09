using System;
using System.Collections.Generic;

namespace BC.API.Services.BookingService
{
  public class AddWorkingWeekReq
  {
    public Guid MasterId { get; set; }

    public DateTime MondayDate { get; set; }

    public DateTime? MondayDateOfPausesDonorWeek { get; set; }

    public List<DayOfWeek> DaysToWork { get; set; }

    public string StartTime { get; set; }

    public string EndTime { get; set; }
  }
}
