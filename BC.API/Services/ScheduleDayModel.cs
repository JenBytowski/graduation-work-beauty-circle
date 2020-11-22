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

    public List<ScheduleDayModelItem> Items { get; set; }

    public int TimeStepInMinutes { get; set; }

    public int ConnectionGapInMinutes { get; set; }

    public int SpaceGapInMinutes { get; set; }

    public bool ConnectedBookingsOnly { get; set; }

    public ScheduleDayModel(ScheduleDay day,
      (int TimeStepInMinutes, int ConnectionGapInMinutes, int SpaceGapInMinutes, bool ConnectedBookingsOnly)
        scheduleParams)
    {
      Id = day.Id;
      ScheduleId = day.ScheduleId;
      Date = day.Date;
      Items = day.Items.Select(itm =>
      {
        if (itm is Booking)
        {
          return new BookingModel {Id = itm.Id, StartTime = itm.StartTime, EndTime = itm.EndTime} as ScheduleDayModelItem;
        }
      
        if (itm is Pause)
        {
          return new PauseModel {Id = itm.Id, StartTime = itm.StartTime, EndTime = itm.EndTime};
        }
        
        return new WindowModel {Id = itm.Id, StartTime = itm.StartTime, EndTime = itm.EndTime};
      }).ToList();
      TimeStepInMinutes = scheduleParams.TimeStepInMinutes;
      ConnectionGapInMinutes = scheduleParams.ConnectionGapInMinutes;
      SpaceGapInMinutes = scheduleParams.SpaceGapInMinutes;
      ConnectedBookingsOnly = scheduleParams.ConnectedBookingsOnly;
    }

    //TODO написать логику
    public ScheduleDay ParseToScheduleDay()
    {
      return new ScheduleDay {Id = Id, ScheduleId = ScheduleId, Date = Date, Items = null};
    }

    public void AddBooking(DateTime startTime, TimeSpan serviceTimeDuration)
    {
      var windows = Items.Where(itm => itm is WindowModel);
      var windowtoRemove =
        windows.FirstOrDefault(
          wind => wind.StartTime <= startTime && wind.EndTime >= startTime.Add(serviceTimeDuration));

      if (windowtoRemove == null)
      {
        throw new CantFoundWindowByTimeException("Dont found window by this time");
      }

      var newBooking = new BookingModel {StartTime = startTime, EndTime = startTime.Add(serviceTimeDuration)};

      var newWindows = new List<WindowModel>
      {
        windowtoRemove.StartTime != startTime
          ? new WindowModel {StartTime = windowtoRemove.StartTime, EndTime = startTime}
          : null,
        windowtoRemove.EndTime != startTime.Add(serviceTimeDuration)
          ? new WindowModel {StartTime = startTime.Add(serviceTimeDuration), EndTime = windowtoRemove.EndTime}
          : null
      }.Where(wind => wind != null).ToList();

      Items.Remove(windowtoRemove);
      Items.Add(newBooking);
      Items.AddRange(newWindows);
    }

    public void CancelBooking(DateTime startTime, TimeSpan serviceTimeDuration)
    {
      var bookingToRemove = Items.Where(itm => itm is BookingModel).SingleOrDefault(bk =>
        bk.StartTime == startTime && bk.EndTime == startTime.Add(serviceTimeDuration));

      if (bookingToRemove == null)
      {
        throw new BookingException($"Dont found booking by this time"); 
      }

      Items.Remove(bookingToRemove);
      Items.Add(new WindowModel
      {
        StartTime = startTime, EndTime = startTime.Add(serviceTimeDuration)
      });

      ConcatenateWindows();
    }

    public void AddPause(DateTime startTime, TimeSpan serviceTimeDuration)
    {
      var windows = Items.Where(itm => itm is WindowModel);
      var windowtoRemove =
        windows.FirstOrDefault(
          wind => wind.StartTime <= startTime && wind.EndTime >= startTime.Add(serviceTimeDuration));

      if (windowtoRemove == null)
      {
        throw new CantFoundWindowByTimeException("Dont found window by this time");
      }

      var newPause = new PauseModel {StartTime = startTime, EndTime = startTime.Add(serviceTimeDuration)};

      var newWindows = new List<WindowModel>
      {
        windowtoRemove.StartTime != startTime
          ? new WindowModel {StartTime = windowtoRemove.StartTime, EndTime = startTime}
          : null,
        windowtoRemove.EndTime != startTime.Add(serviceTimeDuration)
          ? new WindowModel {StartTime = startTime.Add(serviceTimeDuration), EndTime = windowtoRemove.EndTime}
          : null
      }.Where(wind => wind != null).ToList();

      Items.Remove(windowtoRemove);
      Items.Add(newPause);
      Items.AddRange(newWindows);
    }

    public void CancelPause(DateTime startTime, TimeSpan serviceTimeDuration)
    {
      var pauseToRemove = Items.Where(itm => itm is PauseModel).SingleOrDefault(ps =>
        ps.StartTime == startTime && ps.EndTime == startTime.Add(serviceTimeDuration));

      if (pauseToRemove == null)
      {
        throw new BookingException($"Dont found pause by this time"); 
      }

      Items.Remove(pauseToRemove);
      Items.Add(new WindowModel
      {
        StartTime = startTime, EndTime = startTime.Add(serviceTimeDuration)
      });

      ConcatenateWindows();
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
    
    private void ConcatenateWindows()
    {
      var windows = Items.Where(imt => imt is WindowModel);
      var newWindows = windows.ToList();

      Items.RemoveAll(itm => itm is WindowModel);

      for (var counter = 0; counter < newWindows.Count;)
      {
        var window = newWindows[counter];
        var windowtoConcatenate = newWindows.FirstOrDefault(wnd => window.EndTime == wnd.StartTime || window.StartTime == wnd.EndTime);

        if (windowtoConcatenate == null)
        {
          counter++;
        }

        newWindows[counter] = new WindowModel
        {
          Id = window.Id,
          StartTime = window.StartTime,
          EndTime = windowtoConcatenate.EndTime
        };
        newWindows.Remove(windowtoConcatenate);
      }

      Items.AddRange(newWindows);
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
    
  }

  public class WindowModel : ScheduleDayModelItem
  {
  }
}
