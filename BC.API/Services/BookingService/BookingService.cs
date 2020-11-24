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
        .SingleOrDefault(sch => sch.MasterId == req.MasterId);

      if (schedule == null)
      {
        throw new BookingException("Dont found schedule for this master");
      }

      var day = schedule.Days.SingleOrDefault(day => day.Date == req.StartTime.Date);

      if (day == null)
      {
        throw new BookingException("Dont found this day in masters schedule");
      }

      var dayModel = CreateScheduleDayModel(day, schedule.TimeStepInMinutes, schedule.ConnectionGapInMinutes,
        schedule.SpaceGapInMinutes, schedule.ConnectedBookingsOnly);

      var newBooking = new Booking
      {
        ScheduleDayId = day.Id,
        ClientId = req.ClientId,
        StartTime = req.StartTime,
        EndTime = req.EndTime,
        ServiceTypeId = req.ServiceType,
        Description = req.Description
      };

      dayModel.AddBooking(newBooking.StartTime, newBooking.EndTime, newBooking);

      FillScheduleDayByModel(day, dayModel);

      _context.SaveChanges();
    }

    public void CancelBooking(CancelBookingReq req)
    {
      var booking = _context.ScheduleDayItems.FirstOrDefault(bk => bk.Id == req.BookingId);

      if (booking == null)
      {
        throw new BookingException($"Dont found booking by id: {req.BookingId}");
      }

      var day = _context.ScheduleDays.Include(day => day.Items).SingleOrDefault(day => day.Id == booking.ScheduleDayId);

      var dayModel = CreateScheduleDayModel(day, default, default,
        default, default);

      dayModel.CancelBooking(booking.StartTime, booking.EndTime);

      FillScheduleDayByModel(day, dayModel);

      _context.SaveChanges();
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
        .SingleOrDefault(sch => sch.MasterId == req.MasterId);

      if (schedule == null)
      {
        throw new BookingException("Dont found schedule for this master");
      }

      var day = schedule.Days.SingleOrDefault(day => day.Date == req.StartTime.Date);

      if (day == null)
      {
        throw new BookingException("Dont found this day in masters schedule");
      }

      var dayModel = CreateScheduleDayModel(day, schedule.TimeStepInMinutes, schedule.ConnectionGapInMinutes,
        schedule.SpaceGapInMinutes, schedule.ConnectedBookingsOnly);

      var newPause = new Pause
      {
        ScheduleDayId = day.Id, StartTime = req.StartTime, EndTime = req.EndTime, Description = req.Description
      };

      dayModel.AddPause(newPause.StartTime, newPause.EndTime, newPause);

      FillScheduleDayByModel(day, dayModel);

      _context.SaveChanges();
    }

    public void CancelPause(CancelPauseReq req)
    {
      var pause = _context.ScheduleDayItems.FirstOrDefault(ps => ps.Id == req.PauseId);

      if (pause == null)
      {
        throw new BookingException($"Dont found pause by id: {req.PauseId}");
      }

      var day = _context.ScheduleDays.Include(day => day.Items).SingleOrDefault(day => day.Id == pause.ScheduleDayId);

      var dayModel = CreateScheduleDayModel(day, default, default,
        default, default);

      dayModel.CancelPause(pause.StartTime, pause.EndTime);

      FillScheduleDayByModel(day, dayModel);

      _context.SaveChanges();
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

      await this._context.Schedules.AddAsync(new Schedule {MasterId = userAssignedToRoleEvent.UserId});

      await this._context.SaveChangesAsync();
    }

    private ScheduleDayModel CreateScheduleDayModel(ScheduleDay scheduleDay, int timeStepInMinutes,
      int connectionGapInMinutes,
      int spaceGapInMinutes, bool connectedBookingsOnly)
    {
      var model = new ScheduleDayModel(scheduleDay.Items.Min(itm => itm.StartTime),
        scheduleDay.Items.Max(itm => itm.EndTime), timeStepInMinutes, connectionGapInMinutes, spaceGapInMinutes,
        connectedBookingsOnly);

      foreach (var item in scheduleDay.Items)
      {
        if (item is Booking)
        {
          model.AddBooking(item.StartTime, item.EndTime, item as Booking);
        }

        if (item is Pause)
        {
          model.AddPause(item.StartTime, item.EndTime, item as Pause);
        }
      }

      return model;
    }

    private void FillScheduleDayByModel(ScheduleDay scheduleDay, ScheduleDayModel model)
    {
      scheduleDay.Items = model.Items.Select(itm =>
      {
        if (itm.AdditionalData is Booking)
        {
          var bookingData = itm.AdditionalData as Booking;

          return new Booking
          {
            Id = bookingData.Id,
            ScheduleDayId = bookingData.ScheduleDayId,
            ClientId = bookingData.ClientId,
            ServiceTypeId = bookingData.ServiceTypeId,
            ServiceTypeName = bookingData.ServiceTypeName,
            StartTime = itm.StartTime,
            EndTime = itm.EndTime,
            PriceMax = bookingData.PriceMax,
            PriceMin = bookingData.PriceMin,
            DurationInMinutesMax = bookingData.DurationInMinutesMax,
            Description = bookingData.Description
          } as ScheduleDayItem;
        }

        if (itm.AdditionalData is Pause)
        {
          var pauseData = itm.AdditionalData as Pause;

          return new Pause
          {
            Id = pauseData.Id,
            ScheduleDayId = pauseData.ScheduleDayId,
            StartTime = itm.StartTime,
            EndTime = itm.EndTime,
            Description = pauseData.Description
          };
        }

        var windowData = itm.AdditionalData as Window;

        return new Window
        {
          ScheduleDayId = windowData?.ScheduleDayId ?? scheduleDay.ScheduleId,
          StartTime = itm.StartTime,
          EndTime = itm.EndTime
        };
      }).ToList();
    }
  }
}
