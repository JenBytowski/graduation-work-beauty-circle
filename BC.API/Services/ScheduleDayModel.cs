using System;
using System.Collections.Generic;
using System.Linq;
using BC.API.Services.BookingService.Data;
using BC.API.Services.BookingService.Exceptions;
using Window = BC.API.Services.MastersListService.Data.Window;

namespace BC.API.Services
{
  public class ScheduleDayModel
  {
    public Guid Id { get; set; }

    public Guid ScheduleId { get; set; }

    public DateTime Date { get; set; }

    public IEnumerable<ScheduleDayModelItem> Items { get; set; }
    
    public int TimeStepInMinutes { get; set; }
    
    public int ConnectionGapInMinutes { get; set; }
    
    public int SpaceGapInMinutes { get; set; }
    
    public bool ConnectedBookingsOnly { get; set; }

    public ScheduleDayModel(ScheduleDay day, (int TimeStepInMinutes, int ConnectionGapInMinutes, int SpaceGapInMinutes, bool ConnectedBookingsOnly) scheduleParams)
    {
      Id = day.Id;
      ScheduleId = day.ScheduleId;
      Date = day.Date;
      // Items = day.Items.Select(itm =>
      // {
      //   if (itm is Booking)
      //   {
      //     return new BookingModel {Id = itm.Id, StartTime = itm.StartTime, EndTime = itm.EndTime};
      //   }
      //
      //   if (itm is Pause)
      //   {
      //     return new PauseModel {Id = itm.Id, StartTime = itm.StartTime, EndTime = itm.EndTime};
      //   }
      //   
      //   return new WindowModel {Id = itm.Id, StartTime = itm.StartTime, EndTime = itm.EndTime};
      // });
      TimeStepInMinutes = scheduleParams.TimeStepInMinutes;
      ConnectionGapInMinutes = scheduleParams.ConnectionGapInMinutes;
      SpaceGapInMinutes = scheduleParams.SpaceGapInMinutes;
      ConnectedBookingsOnly = scheduleParams.ConnectedBookingsOnly;
    }

    //TODO написать логику
    public ScheduleDay ParseToScheduleDay()
    {
      return new ScheduleDay
      {
        Id = Id,
        ScheduleId = ScheduleId,
        Date = Date,
        Items = null
      };
    }

    public void AddBooking(DateTime startTime, DateTime endTime, TimeSpan serviceTimeDuration)
    {
      var windows = Items.Where(itm => itm is WindowModel);
      var windowtoRemove =
        windows.FirstOrDefault(wind => wind.StartTime <= startTime && wind.EndTime >= endTime);

      if (windowtoRemove == null)
      {
        throw new CantFoundWindowByTimeException("Dont found window by this time");
      }

      var newBooking = new BookingModel
      {
        StartTime = startTime,
        EndTime = endTime,
      };

      // var newWindows = new List<Window>
      // {
      //   scheduleDay.Items.FirstOrDefault(itm => itm.EndTime == newBooking.StartTime) == null
      //     ? new Window {ScheduleDayId = scheduleDay.Id, StartTime = windowtoRemove.StartTime, EndTime = req.StartTime}
      //     : null,
      //   scheduleDay.Items.FirstOrDefault(itm => itm.StartTime == newBooking.EndTime) == null
      //     ? new Window {ScheduleDayId = scheduleDay.Id, StartTime = req.EndTime, EndTime = windowtoRemove.EndTime}
      //     : null
      // }.Where(wind => wind != null);
    }
    
    public IEnumerable<WindowModel> GetPreferedWindowsForBooking(Schedule schedule)
    {
      var procedureTimeDuration = new TimeSpan();
      var bookings = Items.Where(itm => itm is BookingModel);

      if (bookings.Any())
      {
        var connectedWindows = Items.Where(itm => itm is WindowModel).Where(wnd =>
            bookings.Any(bck => bck.StartTime - new TimeSpan(0, schedule.ConnectionGapInMinutes, 0) >= wnd.EndTime ||
                                bck.EndTime + new TimeSpan(0, schedule.ConnectionGapInMinutes, 0) <= wnd.StartTime))
          .Where(wnd => wnd.EndTime - wnd.StartTime > procedureTimeDuration);

        return connectedWindows.Select(wnd => wnd as WindowModel);
      }

      return Items.Where(itm => itm is WindowModel)
        .Where(wnd => wnd.EndTime - wnd.StartTime.Date >= procedureTimeDuration).Select(itm => itm as WindowModel);
    }
  }
  
  public class ScheduleDayModelItem
  {
    public Guid Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
  }

  public class PauseModel : ScheduleDayModelItem
  {
  }

  public class BookingModel : ScheduleDayModelItem
  {
    public Guid ServiceTypeId { get; set; }

    public int DurationInMinutesMax { get; set; }
  }

  public class WindowModel : ScheduleDayModelItem
  {
  }
}
