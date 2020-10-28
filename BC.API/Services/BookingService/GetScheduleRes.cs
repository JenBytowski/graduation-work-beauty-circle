using System;
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

    public DateTime Date { get; set; }

    public IEnumerable<ScheduleDayItemRes> Items { get; set; }

    public static ScheduleDayRes ParseFromScheduleDay(ScheduleDay scheduleDay)
    {
      return new ScheduleDayRes
      {
        Id = scheduleDay.Id,
        ScheduleId = scheduleDay.Id,
        Date = scheduleDay.Date,
        Items = scheduleDay.Items.Select(itm => ScheduleDayItemRes.ParseFromScheduleDayItem(itm))
      };
    }
  }

  public class ScheduleDayItemRes
  {
    public Guid Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
    
    public string ItemType { get; set; }

    public Guid? ClientId { get; set; }

    public Guid? ServiceTypeId { get; set; }

    public string Description { get; set; }

    public int? PriceMin { get; set; }

    public int? PriceMax { get; set; }

    public int? DurationInMinutesMax { get; set; }

    public static ScheduleDayItemRes ParseFromScheduleDayItem(ScheduleDayItem scheduleDayItem)
    {
      if (scheduleDayItem is Booking)
      {
        var booking = scheduleDayItem as Booking;
        
        return new ScheduleDayItemRes
        {
          Id = booking.Id,
          StartTime = booking.StartTime,
          EndTime = booking.StartTime,
          ItemType = nameof(Booking),
          ClientId = booking.ClientId,
          Description = booking.Description,
          ServiceTypeId = booking.ServiceTypeId,
          PriceMax = booking.PriceMax,
          PriceMin = booking.PriceMin,
          DurationInMinutesMax = booking.DurationInMinutesMax
        };
      }

      if (scheduleDayItem is Pause)
      {
        var pause = scheduleDayItem as Pause;

        return new ScheduleDayItemRes
        {
          Id = pause.Id,
          StartTime = pause.StartTime,
          EndTime = pause.EndTime,
          ItemType = nameof(Pause),
          Description = pause.Description,
        };
      }

      return new ScheduleDayItemRes
      {
        Id = scheduleDayItem.Id,
        StartTime = scheduleDayItem.StartTime,
        EndTime = scheduleDayItem.EndTime,
        ItemType = nameof(Window),
      };
    }
  }
  
}
