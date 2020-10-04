using System;
using System.Collections.Generic;
using System.Linq;
using BC.API.Events;
using BC.API.Services.BookingService.Data;
using Microsoft.EntityFrameworkCore;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.BookingService
{
  public class BookingService
  {
    private IEventBus _eventBus;

    private BookingContext _context;

    public BookingService(IEventBus eventBus, BookingContext context)
    {
      _eventBus = eventBus;
      _context = context;
    }

    public GetScheduleRes GetSchedule(GetScheduleReq req)
    {
      var schedule = _context.Schedules.Include(sch => sch.Days).ThenInclude(day => day.Items).
        FirstOrDefault(sch => sch.MasterId == req.MasterId);

      if (schedule == null)
      {
        throw new Exception("Dont found schedule for this master.");
      }

      return new GetScheduleRes { Days = schedule.Days };
    }

    public void AddWorkingWeek(AddWorkingWeekReq req)
    {
      var schedule = _context.Schedules.Include(sch => sch.Days).ThenInclude(day => day.Items).
        FirstOrDefault(sch => sch.MasterId == req.MasterId);
      
      if (schedule == null)
      {
        throw new Exception("Dont found schedule for this master.");
      }

      var newWeek = new List<ScheduleDay>();
      
      if (req.MondayDateOfPausesDonorWeek != null)
      {
        var donorWeekMonday = schedule.Days.First(day => day.StartTime.Date == req.MondayDateOfPausesDonorWeek.Value.Date);
        var donorWeek = schedule.Days.Where(day =>
          day.StartTime.Date >= donorWeekMonday.StartTime.Date &&
          day.StartTime.Date < donorWeekMonday.StartTime.AddDays(4).Date).OrderBy(
          day => day.StartTime.Date);

        newWeek = donorWeek.Select((day, i) =>
        {
          var newDate = req.MondayDate.AddDays(i);
          return new ScheduleDay
          {
            ScheduleId = day.ScheduleId,
            DayOfWeek = day.DayOfWeek,
            StartTime = new DateTime(newDate.Year, newDate.Month, newDate.Day, day.StartTime.Hour, day.StartTime.Minute, day.StartTime.Millisecond),
            EndTime = new DateTime(newDate.Year, newDate.Month, newDate.Day, day.EndTime.Hour, day.EndTime.Minute, day.EndTime.Millisecond),
            Items = day.Items
          };
        }).ToList();
      }
      else
      {
        for (var counter = 0; counter < 5; counter++)
        {
          var newDate = req.MondayDate.AddDays(counter);

          newWeek.Add(new ScheduleDay
          {
            ScheduleId = schedule.Id,
            StartTime = new DateTime(newDate.Year, newDate.Month, newDate.Day, 0, 0, 0),
            //TODO пофиксить 
            EndTime = new DateTime(newDate.Year, newDate.Month, newDate.Day, 23, 59, 59),
            Items = new List<ScheduleDayItem>
            {
              new Window
              {
                StartTime = new DateTime(newDate.Year, newDate.Month, newDate.Day, 0, 0, 0),
                EndTime = new DateTime(newDate.Year, newDate.Month, newDate.Day, 23, 59, 59)
              }
            }
          });
        }
      }

      _context.ScheduleDays.AddRange(newWeek);
      _context.SaveChanges();
    }
    
    public void AddBooking(AddBookingReq req)
    {
      var schedule = _context.Schedules.Include(sch => sch.Days).ThenInclude(day => day.Items).
        FirstOrDefault(sch => sch.MasterId == req.MasterId);
      
      if (schedule == null)
      {
        throw new Exception("Dont found schedule for this master.");
      }

      var scheduleDay = schedule.Days.First(day => day.StartTime.Date == req.StartTime.Date);
      var windowses = scheduleDay.Items.Where(itm => itm is Window);
      var windowtoRemove =
        windowses.FirstOrDefault(wind => wind.StartTime <= req.StartTime && wind.EndTime >= req.EndTime);
      
      if (windowtoRemove == null)
      {
        throw new Exception("Dont found window by this time.");
      }

      var newBooking = new Booking
      {
        ClientId = req.ClientId,
        StartTime = req.StartTime,
        EndTime = req.EndTime,
        ServiceTypeId = req.ServiceType,
        Description = req.Description
      };
      var newWindowses = new List<Window>
      {
        new Window
        {
          ScheduleDayId = scheduleDay.Id, 
          StartTime = windowtoRemove.StartTime,
          EndTime = req.StartTime
        },
        new Window
        {
          ScheduleDayId = scheduleDay.Id, 
          StartTime = req.EndTime,
          EndTime = windowtoRemove.EndTime
        }
      }; 
      
      _context.ScheduleDayItems.Remove(windowtoRemove);
      _context.ScheduleDayItems.AddRange(newWindowses);
      _context.ScheduleDayItems.Add(newBooking);
      _context.SaveChanges();
      
      this._eventBus.Publish(new ScheduleDayChangedEvent(){ });
    }

    public void CancelBooking(CancelBookingReq req)
    {
      var booking = _context.ScheduleDayItems.FirstOrDefault(bk => bk.Id == req.BookingId);

      if (booking == null)
      {
        throw new Exception($"Dont found booking by id: {req.BookingId}");
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
    }

    public void AddPause(AddPauseReq req)
    {
      var schedule = _context.Schedules.Include(sch => sch.Days).ThenInclude(day => day.Items).
        FirstOrDefault(sch => sch.MasterId == req.MasterId);
      
      if (schedule == null)
      {
        throw new Exception("Dont found schedule for this master.");
      }

      var scheduleDay = schedule.Days.First(day => day.StartTime.Date == req.StartTime.Date);
      var windowses = scheduleDay.Items.Where(itm => itm is Window);
      var windowtoRemove =
        windowses.FirstOrDefault(wind => wind.StartTime <= req.StartTime && wind.EndTime >= req.EndTime);
      
      if (windowtoRemove == null)
      {
        throw new Exception("Dont found window by this time.");
      }

      var newPause = new Pause
      {
        ScheduleDayId = schedule.Id,
        StartTime = req.StartTime,
        EndTime = req.EndTime,
        Description = req.Description
      };
      var newWindowses = new List<Window>
      {
        new Window
        {
          ScheduleDayId = scheduleDay.Id, 
          StartTime = windowtoRemove.StartTime,
          EndTime = req.StartTime
        },
        new Window
        {
          ScheduleDayId = scheduleDay.Id, 
          StartTime = req.EndTime,
          EndTime = windowtoRemove.EndTime
        }
      };
      
      _context.ScheduleDayItems.Remove(windowtoRemove);
      _context.ScheduleDayItems.AddRange(newWindowses);
      _context.ScheduleDayItems.Add(newPause);
      _context.SaveChanges();
    }

    public void CancelPause(CancelPauseReq req)
    {
      var pause = _context.ScheduleDayItems.FirstOrDefault(ps => ps.Id == req.PauseId);

      if (pause == null)
      {
        throw new Exception($"Dont found booking by id: {req.PauseId}");
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
    }
  }

  public class AddWorkingWeekReq
  {
    public Guid MasterId { get; set; }
    
    public DateTime MondayDate { get; set; }
    
    public DateTime? MondayDateOfPausesDonorWeek { get; set; }
    
    public List<DayOfWeek> DaysToWork { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
  }

  public class GetScheduleReq
  {
    public Guid MasterId { get; set; }
  }

  public class GetScheduleRes
  {
    public IEnumerable<ScheduleDay> Days { get; set; }
  }

  public class CancelPauseReq
  {
    public Guid PauseId { get; set; }
  }

  public class AddPauseReq
  {
    public Guid MasterId { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public string Description { get; set; }
  }

  public class CancelBookingReq
  {
    public Guid BookingId { get; set; }
  }

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
