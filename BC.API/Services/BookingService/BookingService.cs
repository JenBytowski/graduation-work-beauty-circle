using System;
using System.Collections.Generic;
using BC.API.Events;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.BookingService
{
  public class BookingService
  {
    private IEventBus _eventBus;

    public BookingService(IEventBus eventBus)
    {
      _eventBus = eventBus;
    }

    public GetScheduleRes GetSchedule(GetScheduleReq req)
    {
      return new GetScheduleRes { };
    }

    public void AddWorkingWeek(AddWorkingWeekReq req)
    {
    }

    public void AddBooking(AddBookingReq req)
    {
      this._eventBus.Publish(new ScheduleDayChangedEvent(){ });
    }

    public void CancelBooking(CancelBookingReq req)
    {
    }

    public void AddPause(AddPauseReq req)
    {
    }

    public void CancelPause(CancelPauseReq req)
    {
    }
  }

  public class AddWorkingWeekReq
  {
    public DateTime MondayDate { get; set; }
    public DateTime? MondayDateOfPausesDonorWeek { get; set; }
    public List<DayOfWeek> DaysToWork { get; set; }
  }

  public class GetScheduleReq
  {
    public Guid MasterId { get; set; }
  }

  public class GetScheduleRes
  {
  }

  public class CancelPauseReq
  {
    public Guid PauseId { get; set; }
  }

  public class AddPauseReq
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }

  public class CancelBookingReq
  {
    public Guid BookingId { get; set; }
  }

  public class AddBookingReq
  {
    public Guid ClientId { get; set; }
    public Guid ServiceType { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
  }
}
