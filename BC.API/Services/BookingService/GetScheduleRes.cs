﻿using System;
using System.Collections.Generic;
using System.Linq;
using BC.API.Services.BookingService.Data;

namespace BC.API.Services.BookingService
{
  public class GetScheduleRes
  {
    public IEnumerable<ScheduleDayRes> Days { get; set; }
  }

  public class ScheduleDayRes
  {
    public Guid Id { get; set; }

    public Guid ScheduleId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public IEnumerable<ScheduleDayItemRes> Items { get; set; }

    public static ScheduleDayRes ParseFromScheduleDay(ScheduleDay scheduleDay)
    {
      return new ScheduleDayRes
      {
        Id = scheduleDay.Id,
        ScheduleId = scheduleDay.Id,
        StartTime = scheduleDay.StartTime,
        EndTime = scheduleDay.EndTime,
        Items = scheduleDay.Items.Select(itm => ScheduleDayItemRes.ParseFromScheduleDayItem(itm))
      };
    }
  }

  public class ScheduleDayItemRes
  {
    public Guid Id { get; set; }

    public Guid ScheduleDayId { get; set; }

    public ScheduleDayRes ScheduleDayRes { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public static ScheduleDayItemRes ParseFromScheduleDayItem(ScheduleDayItem scheduleDayItem)
    {
      if (scheduleDayItem is Booking)
      {
        return BookingRes.ParseFromBooking(scheduleDayItem as Booking);
      }

      if (scheduleDayItem is Pause)
      {
        return PauseRes.ParseFromPause(scheduleDayItem as Pause);
      }

      return WindowRes.ParseFromWindow(scheduleDayItem as Window);
    }
  }

  public class PauseRes : ScheduleDayItemRes
  {
    public string Description { get; set; }

    public static PauseRes ParseFromPause(Pause pause)
    {
      return new PauseRes
      {
        Id = pause.Id,
        ScheduleDayId = pause.ScheduleDayId,
        StartTime = pause.StartTime,
        EndTime = pause.EndTime,
        Description = pause.Description,
      };
    }
  }

  public class BookingRes : ScheduleDayItemRes
  {
    public Guid ClientId { get; set; }

    public Guid ServiceTypeId { get; set; }

    public string ServiceTypeName { get; set; }

    public string Description { get; set; }

    public int PriceMin { get; set; }

    public int PriceMax { get; set; }

    public int DurationInMinutesMax { get; set; }

    public static BookingRes ParseFromBooking(Booking booking)
    {
      return new BookingRes
      {
        Id = booking.Id,
        ScheduleDayId = booking.ScheduleDayId,
        ClientId = booking.ClientId,
        StartTime = booking.StartTime,
        EndTime = booking.EndTime,
        ServiceTypeId = booking.ServiceTypeId,
        ServiceTypeName = booking.ServiceTypeName,
        DurationInMinutesMax = booking.DurationInMinutesMax,
        PriceMax = booking.PriceMax,
        PriceMin = booking.PriceMin,
        Description = booking.Description
      };
    }
  }

  public class WindowRes : ScheduleDayItemRes
  {
    public static WindowRes ParseFromWindow(Window window)
    {
      return new WindowRes
      {
        Id = window.Id, ScheduleDayId = window.ScheduleDayId, StartTime = window.StartTime, EndTime = window.EndTime
      };
    }
  }
}