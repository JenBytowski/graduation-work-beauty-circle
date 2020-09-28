﻿using System;
using System.Collections.Generic;
using System.Linq;
using BC.API.Services.MastersListService.Data;
using ListItem = BC.API.Services.MastersListService.Data.PriceListItem;
using Day = BC.API.Services.MastersListService.Data.ScheduleDay;

namespace BC.API.Services.MastersListService
{
  internal class MasterRes
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

    public IEnumerable<PriceListItem> PriceList { get; set; }

    public IEnumerable<ScheduleDay> Schedule { get; set; }

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
        PriceList = master.PriceList.PriceListItems.Select(itm => PriceListItem.ParseFromPriseListItem(itm)),
        Schedule = master.Schedule.Days.Select(day => ScheduleDay.ParseFromScheduleDay(day)),
        AverageRating = master.Stars / master.ReviewsCount
      };
    }
  }

  internal class ScheduleDay
  {
    public DayOfWeek DayOfWeek { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public IEnumerable<SchedulePause> Pauses { get; set; }

    public IEnumerable<ScheduleBooking> Bookings { get; set; }

    public static ScheduleDay ParseFromScheduleDay(Day scheduleDay)
    {
      return new ScheduleDay
      {
        DayOfWeek = scheduleDay.DayOfWeek,
        StartTime = scheduleDay.StartTime,
        EndTime = scheduleDay.EndTime,
        Pauses = scheduleDay.Pauses.Select(ps => SchedulePause.ParseFromPause(ps)),
        Bookings = scheduleDay.Bookings.Select(book => ScheduleBooking.ParseFromBooking(book))
      };
    }
  }

  internal class ScheduleBooking
  {
    public DateTime StartTime { get; set;}

    public DateTime EndTime { get; set; }

    public static ScheduleBooking ParseFromBooking(Booking booking)
    {
      return new ScheduleBooking
      {
        StartTime = booking.StartTime,
        EndTime = booking.EndTime
      };
    }
  }

  internal class SchedulePause
  {
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public static SchedulePause ParseFromPause(Pause pause)
    {
      return new SchedulePause
      {
        StartTime = pause.StartTime,
        EndTime = pause.EndTime
      };
    }
  }

  internal class PriceListItem
  {
    public string Name { get; set; }

    public int PriceMin { get; set; }

    public int PriceMax { get; set; }

    public int DurationInMinutesMax { get; set; }

    public static PriceListItem ParseFromPriseListItem(ListItem priceListItem)
    {
      return new PriceListItem
      {
        Name = priceListItem.ServiceType.Name,
        PriceMin = priceListItem.PriceMin,
        PriceMax = priceListItem.PriceMax,
        DurationInMinutesMax = priceListItem.DurationInMinutesMax
      };
    }
  }
}
