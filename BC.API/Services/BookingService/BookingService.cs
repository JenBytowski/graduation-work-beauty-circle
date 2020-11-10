using System;
using System.Collections.Generic;
using System.Linq;
using BC.API.Services.BookingService.Data;
using BC.API.Services.BookingService.Exceptions;
using Microsoft.EntityFrameworkCore;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.BookingService
{
  public class BookingService
  {
    private IEventBus _eventBus;

    private BookingContext _context;

    public BookingService(BookingContext context)
    {
      _context = context;
    }

    public GetScheduleRes GetSchedule(Guid MasterId)
    {
      var schedule = _context.Schedules.Include(sch => sch.Days).ThenInclude(day => day.Items)
        .FirstOrDefault(sch => sch.MasterId == MasterId);

      if (schedule == null)
      {
        throw new BookingException("Dont found schedule for this master");
      }

      return new GetScheduleRes {Days = schedule.Days.Select(day => ScheduleDayRes.ParseFromScheduleDay(day))};
    }

    public void AddWorkingWeek(AddWorkingWeekReq req)
    {
      var schedule = _context.Schedules.Include(sch => sch.Days).ThenInclude(day => day.Items)
        .FirstOrDefault(sch => sch.MasterId == req.MasterId);

      if (schedule == null)
      {
        throw new BookingException("Dont found schedule for this master");
      }

      var newWeek = new List<ScheduleDay>();

      if (req.MondayDateOfPausesDonorWeek != null)
      {
        var donorWeekMonday =
          schedule.Days.First(day => day.Date == req.MondayDateOfPausesDonorWeek.Value.Date);
        var donorWeek = schedule.Days.Where(day =>
          day.Date >= donorWeekMonday.Date &&
          day.Date < donorWeekMonday.Date.AddDays(6).Date).OrderBy(
          day => day.Date);

        newWeek = donorWeek.Select((day, i) =>
        {
          var newDate = req.MondayDate.AddDays(i);
          
          return new ScheduleDay
          {
            ScheduleId = day.ScheduleId,
            Date = new DateTime(newDate.Year, newDate.Month, newDate.Day),
            Items = day.Items
          };
        }).ToList();
      }
      else
      {
        req.DaysToWork.ForEach(day =>
        {
          var newDate = req.MondayDate.AddDays(day != DayOfWeek.Sunday ? (int)day - 1 : 6);
          var startTime = TimeSpan.Parse(req.StartTime);
          var endTime = TimeSpan.Parse(req.EndTime);

          newWeek.Add(new ScheduleDay
          {
            ScheduleId = schedule.Id,
            Date = new DateTime(newDate.Year, newDate.Month, newDate.Day),
            Items = new List<ScheduleDayItem>
            {
              new Window
              {
                StartTime = new DateTime(newDate.Year, newDate.Month, newDate.Day, startTime.Hours,
                  startTime.Minutes, 0),
                EndTime = new DateTime(newDate.Year, newDate.Month, newDate.Day, endTime.Hours,
                  endTime.Minutes, 0)
              }
            }
          });
        });
      }

      _context.ScheduleDays.AddRange(newWeek);
      _context.SaveChanges();
    }

    public void AddBooking(AddBookingReq req)
    {
      var schedule = _context.Schedules.Include(sch => sch.Days).ThenInclude(day => day.Items)
        .FirstOrDefault(sch => sch.MasterId == req.MasterId);

      if (schedule == null)
      {
        throw new BookingException("Dont found schedule for this master");
      }

      var scheduleDay = schedule.Days.First(day => day.Date == req.StartTime.Date);
      var windows = scheduleDay.Items.Where(itm => itm is Window);
      var windowtoRemove =
        windows.FirstOrDefault(wind => wind.StartTime <= req.StartTime && wind.EndTime >= req.EndTime);

      if (windowtoRemove == null)
      {
        throw new CantFoundWindowByTimeException("Dont found window by this time");
      }

      var newBooking = new Booking
      {
        ScheduleDayId = scheduleDay.Id,
        ClientId = req.ClientId,
        StartTime = req.StartTime,
        EndTime = req.EndTime,
        ServiceTypeId = req.ServiceType,
        Description = req.Description
      };

      var newWindows = new List<Window>
      {
        scheduleDay.Items.FirstOrDefault(itm => itm.EndTime == newBooking.StartTime) == null
          ? new Window {ScheduleDayId = scheduleDay.Id, StartTime = windowtoRemove.StartTime, EndTime = req.StartTime}
          : null,
        scheduleDay.Items.FirstOrDefault(itm => itm.StartTime == newBooking.EndTime) == null
          ? new Window {ScheduleDayId = scheduleDay.Id, StartTime = req.EndTime, EndTime = windowtoRemove.EndTime}
          : null
      }.Where(wind => wind != null);

      _context.ScheduleDayItems.Remove(windowtoRemove);
      _context.ScheduleDayItems.AddRange(newWindows);
      _context.ScheduleDayItems.Add(newBooking);
      _context.SaveChanges();

      //this._eventBus.Publish(new ScheduleDayChangedEvent() { });
    }

    public void CancelBooking(CancelBookingReq req)
    {
      var booking = _context.ScheduleDayItems.FirstOrDefault(bk => bk.Id == req.BookingId);

      if (booking == null)
      {
        throw new BookingException($"Dont found booking by id: {req.BookingId}");
      }

      var newWindow = new Window
      {
        ScheduleDayId = booking.ScheduleDayId, StartTime = booking.StartTime, EndTime = booking.EndTime
      };

      _context.ScheduleDayItems.Remove(booking);
      _context.ScheduleDayItems.Add(newWindow);
      _context.SaveChanges();

      ConcatenateWindows(booking.ScheduleDayId);
    }

    public void AddPause(AddPauseReq req)
    {
      var schedule = _context.Schedules.Include(sch => sch.Days).ThenInclude(day => day.Items)
        .FirstOrDefault(sch => sch.MasterId == req.MasterId);

      if (schedule == null)
      {
        throw new BookingException("Dont found schedule for this master");
      }

      var scheduleDay = schedule.Days.First(day => day.Date == req.StartTime.Date);
      var windows = scheduleDay.Items.Where(itm => itm is Window);
      var windowtoRemove =
        windows.FirstOrDefault(wind => wind.StartTime <= req.StartTime && wind.EndTime >= req.EndTime);

      if (windowtoRemove == null)
      {
        throw new CantFoundWindowByTimeException("Dont found window by this time");
      }

      var newPause = new Pause
      {
        ScheduleDayId = scheduleDay.Id,
        StartTime = req.StartTime,
        EndTime = req.EndTime,
        Description = req.Description
      };
      var newWindows = new List<Window>
      {
        scheduleDay.Items.FirstOrDefault(itm => itm.EndTime == newPause.StartTime) == null
          ? new Window {ScheduleDayId = scheduleDay.Id, StartTime = windowtoRemove.StartTime, EndTime = req.StartTime}
          : null,
        scheduleDay.Items.FirstOrDefault(itm => itm.StartTime == newPause.EndTime) == null
          ? new Window {ScheduleDayId = scheduleDay.Id, StartTime = req.EndTime, EndTime = windowtoRemove.EndTime}
          : null
      }.Where(wind => wind != null);

      _context.ScheduleDayItems.Remove(windowtoRemove);
      _context.ScheduleDayItems.AddRange(newWindows);
      _context.ScheduleDayItems.Add(newPause);
      _context.SaveChanges();
    }

    public void CancelPause(CancelPauseReq req)
    {
      var pause = _context.ScheduleDayItems.FirstOrDefault(ps => ps.Id == req.PauseId);

      if (pause == null)
      {
        throw new BookingException($"Dont found pause by id: {req.PauseId}");
      }

      var newWindow = new Window
      {
        ScheduleDayId = pause.ScheduleDayId, StartTime = pause.StartTime, EndTime = pause.EndTime
      };

      _context.ScheduleDayItems.Remove(pause);
      _context.ScheduleDayItems.Add(newWindow);
      _context.SaveChanges();

      ConcatenateWindows(pause.ScheduleDayId);
    }
    
    private void ConcatenateWindows(Guid dayId)
    {
      var day = _context.ScheduleDays.Include(day => day.Items).First(day => day.Id == dayId);
      var windows = day.Items.Where(imt => imt is Window);
      var newWindows = windows.ToList();

      _context.ScheduleDayItems.RemoveRange(windows);
      _context.SaveChanges();

      for (var counter = 0; counter < newWindows.Count;)
      {
        var window = newWindows[counter];
        var windowtoConcatenate = newWindows.FirstOrDefault(wnd => window.EndTime == wnd.StartTime || window.StartTime == wnd.EndTime);

        if (windowtoConcatenate == null)
        {
          counter++;
        }

        newWindows[counter] = new Window
        {
          Id = window.Id,
          ScheduleDayId = window.ScheduleDayId,
          StartTime = window.StartTime,
          EndTime = windowtoConcatenate.EndTime
        };
        newWindows.Remove(windowtoConcatenate);
      }

      _context.ScheduleDayItems.AddRange(newWindows);
      _context.SaveChanges();
    }
  }
}
