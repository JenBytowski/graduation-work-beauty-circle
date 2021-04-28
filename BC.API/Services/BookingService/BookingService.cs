using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC.API.Events;
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

      return new GetScheduleRes { Days = schedule.Days.Select(day => ScheduleDayRes.ParseFromScheduleDay(day)) };
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
        windows.FirstOrDefault(wind => wind.StartTime <= req.StartTime && wind.EndTime >= req.EndTime) as Window;

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

      _context.ScheduleDayItems.Remove(windowtoRemove);
      _context.ScheduleDayItems.AddRange(this.DivideWindow(windowtoRemove, newBooking));
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
        ScheduleDayId = booking.ScheduleDayId,
        StartTime = booking.StartTime,
        EndTime = booking.EndTime
      };

      _context.ScheduleDayItems.Remove(booking);
      _context.ScheduleDayItems.Add(newWindow);
      _context.SaveChanges();

      ConcatenateWindows(booking.ScheduleDayId);
    }

    public async Task<BookingRes> GetBooking(Guid bookingId)
    {
      try
      {
        var booking = await _context.Bookings
          .Include(b => b.ScheduleDay)
          .ThenInclude(sd => sd.Schedule)
          .SingleAsync(b => b.Id == bookingId);

        var bookingRes = BookingRes.ParseFromScheduleDayItem(booking);

        return bookingRes;
      }
      catch (Exception exception)
      {
        throw new BookingException("Booking not found");
      }
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
        windows.FirstOrDefault(wind => wind.StartTime <= req.StartTime && wind.EndTime >= req.EndTime) as Window;

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

      _context.ScheduleDayItems.Remove(windowtoRemove);
      _context.ScheduleDayItems.AddRange(this.DivideWindow(windowtoRemove, newPause));
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
        ScheduleDayId = pause.ScheduleDayId,
        StartTime = pause.StartTime,
        EndTime = pause.EndTime
      };

      _context.ScheduleDayItems.Remove(pause);
      _context.ScheduleDayItems.Add(newWindow);
      _context.SaveChanges();

      ConcatenateWindows(pause.ScheduleDayId);
    }

    private IEnumerable<Window> DivideWindow(Window window, ScheduleDayItem separator)
    {
      var result = new List<Window>();

      if (window.ScheduleDayId != separator.ScheduleDayId)
      {
        throw new ArgumentException($"{nameof(DivideWindow)} schedule day id is mismatched");
      }

      if (window.StartTime != separator.StartTime)
      {
        result.Add(new Window
        {
          ScheduleDayId = window.ScheduleDayId,
          StartTime = window.StartTime,
          EndTime = separator.StartTime
        });
      }

      if (window.EndTime != separator.EndTime)
      {
        result.Add(new Window
        {
          ScheduleDayId = window.ScheduleDayId,
          StartTime = separator.EndTime,
          EndTime = window.EndTime
        });
      }

      return result;
    }

    private void ConcatenateWindows(Guid dayId)
    {
      this._context.ScheduleDays.Where(day => day.Id == dayId).Load();
      var day = this._context.ScheduleDays.Local.SingleOrDefault(day => day.Id == dayId);

      if (day == null)
      {
        throw new BookingException($"cant find day with id: {dayId}");
      }

      this._context.Entry(day).Collection(day => day.Items).Load();
      var dayItems = this._context.ScheduleDayItems.Local.Where(itm => itm.ScheduleDayId == dayId);

      if (dayItems.Count() == 0)
      {
        throw new BookingException($"day with id: {dayId} dont contains any day items");
      }

      var windows = dayItems.Where(itm => itm is Window);
      var result = new List<ScheduleDayItem>();

      foreach (var window in windows)
      {
        var windowsToConcatenate = windows.Where(wnd =>
        (window.StartTime == wnd.EndTime || window.EndTime == wnd.StartTime))
          .ToList();

        if (result.Any(itm => window.StartTime >= itm.StartTime && window.EndTime <= itm.EndTime))
        {
          continue;
        }

        if (windowsToConcatenate.Count() == 0)
        {
          result.Add(window);

          continue;
        }

        windowsToConcatenate.Add(window);
        var newWindow = new Window
        {
          ScheduleDayId = window.ScheduleDayId,
          StartTime = windowsToConcatenate.Min(wnd => wnd.StartTime),
          EndTime = windowsToConcatenate.Max(wnd => wnd.EndTime)
        };
        result.Add(newWindow);
      }

      var windowsToAdd = result.Where(itm => !windows.Any(wnd => wnd.Id == itm.Id));
      var windowsToRemove = windows.Where(wnd => result.Any(itm => itm.Id == wnd.Id) ||
      result.Any(itm => wnd.StartTime >= itm.StartTime && wnd.EndTime <= itm.EndTime));

      this._context.RemoveRange(windowsToRemove);
      this._context.AddRange(windowsToAdd);
      this._context.SaveChanges();
    }

    public async Task OnUserAssignedToRole(UserAssignedToRoleEvent userAssignedToRoleEvent)
    {
      if (userAssignedToRoleEvent.Role != Domain.UserRoles.Master)
      {
        return;
      }

      if (this._context.Schedules.Any(s => s.MasterId == userAssignedToRoleEvent.UserId))
      {
        return;
      }

      await this._context.Schedules.AddAsync(new Schedule
      {
        MasterId = userAssignedToRoleEvent.UserId
      });

      await this._context.SaveChangesAsync();
    }
  }
}
