using System;
using System.Collections.Generic;
using System.Linq;
using BC.API.Services.MastersListService.Data;

namespace BC.API.Services.MastersListService
{
  public class MasterRes
  {
    public string Name { get; set; }

    public string CityId { get; set; }

    public string AvatarUrl { get; set; }

    public string About { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public string InstagramProfile { get; set; }

    public string VkProfile { get; set; }

    public string Viber { get; set; }

    public string Skype { get; set; }

    public string Speciality { get; set; }

    public IEnumerable<PriceListItemRes> PriceList { get; set; }

    public IEnumerable<ScheduleDayRes> Schedule { get; set; }

    public double AverageRating { get; set; }

    public static MasterRes ParseFromMaster(Master master)
    {
      return new MasterRes
      {
        Name = master.Name,
        CityId = master.CityId.ToString(),
        AvatarUrl = master.AvatarUrl,
        About = master.About,
        Address = master.Address,
        Phone = master.Phone,
        InstagramProfile = master.InstagramProfile,
        VkProfile = master.VkProfile,
        Viber = master.Viber,
        Skype = master.Skype,
        Speciality = master.Speciality.Name,
        PriceList = master.PriceList?.PriceListItems.Select(itm => PriceListItemRes.ParseFromPriseListItem(itm)),
        Schedule = master.Schedule?.Days.Select(day => ScheduleDayRes.ParseFromScheduleDay(day)),
        AverageRating = master.Stars / master.ReviewsCount
      };
    }
  }

  public class ScheduleDayRes
  {
    public DayOfWeek DayOfWeek { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    // public IEnumerable<SchedulePause> Pauses { get; set; }
    //
    // public IEnumerable<ScheduleBooking> Bookings { get; set; }
    
    public IEnumerable<Window> Windows { get; set; }

    public static ScheduleDayRes ParseFromScheduleDay(ScheduleDay scheduleDay)
    {
      return new ScheduleDayRes
      {
        DayOfWeek = scheduleDay.DayOfWeek,
        StartTime = scheduleDay.StartTime,
        EndTime = scheduleDay.EndTime,
        // Pauses = scheduleDay.Pauses.Select(ps => SchedulePause.ParseFromPause(ps)),
        // Bookings = scheduleDay.Bookings.Select(book => ScheduleBooking.ParseFromBooking(book))
        Windows = scheduleDay.Items.Where(i => i is Data.Window).Select(i => Window.ParseFromWindow(i as Data.Window))
      };
    }
  }

  public class Window
  {
    public DateTime StartTime { get; set;}
    public DateTime EndTime { get; set; }

    public static Window ParseFromWindow(Data.Window window)
    {
      return new Window()
      {
        StartTime = window.StartTime,
        EndTime = window.EndTime,
      };
    }
  }

  // public class ScheduleBooking
  // {
  //   public DateTime StartTime { get; set;}
  //
  //   public DateTime EndTime { get; set; }
  //
  //   public static ScheduleBooking ParseFromBooking(Booking booking)
  //   {
  //     return new ScheduleBooking
  //     {
  //       StartTime = booking.StartTime,
  //       EndTime = booking.EndTime
  //     };
  //   }
  // }
  //
  // public class SchedulePause
  // {
  //   public DateTime StartTime { get; set; }
  //
  //   public DateTime EndTime { get; set; }
  //
  //   public static SchedulePause ParseFromPause(Pause pause)
  //   {
  //     return new SchedulePause
  //     {
  //       StartTime = pause.StartTime,
  //       EndTime = pause.EndTime
  //     };
  //   }
  // }

  public class PriceListItemRes
  {
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public ServiceTypeRes ServiceType { get; set; }

    public int PriceMin { get; set; }

    public int PriceMax { get; set; }

    public int DurationInMinutesMax { get; set; }

    public static PriceListItemRes ParseFromPriseListItem(PriceListItem priceListItem)
    {
      return new PriceListItemRes
      {
        Id = priceListItem.Id,
        Name = priceListItem.ServiceType.Name,
        ServiceType = ServiceTypeRes.ParseFromServiceType(priceListItem.ServiceType),
        PriceMin = priceListItem.PriceMin,
        PriceMax = priceListItem.PriceMax,
        DurationInMinutesMax = priceListItem.DurationInMinutesMax
      };
    }
  }
  
  public class ServiceTypeRes
  {
    public string Name { get; set; }
    
    public Guid ServiceTypeSubGroupId { get; set; }
    
    public static ServiceTypeRes ParseFromServiceType(ServiceType serviceType)
    {
      return new ServiceTypeRes
      {
        Name = serviceType.Name,
        ServiceTypeSubGroupId = serviceType.ServiceTypeSubGroupId
      };
    }
  }
}
