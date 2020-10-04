using System;
using System.Collections.Generic;

namespace BC.API.Services.BookingService.Model
{
  public class ScheduleDay
  {
    //TODO все логика работы с расписанием вынести все что есть из сервиса 
    public Guid Id { get; set; }

    public Guid ScheduleId { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    //public IList<ScheduleDayItem> Items { get; set; }
  }
}
